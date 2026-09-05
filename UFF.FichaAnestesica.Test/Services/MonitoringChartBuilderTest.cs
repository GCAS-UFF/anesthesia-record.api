using Microsoft.Extensions.Logging.Abstractions;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Response.Print;
using UFF.FichaAnestesica.Infra.Services;

namespace UFF.FichaAnestesica.Test.Services
{
    public class MonitoringChartBuilderTest
    {
        private static readonly DateOnly BaseDate = new(2026, 3, 10);

        private static DateTime OnBaseDate(int hour, int minute) =>
            BaseDate.ToDateTime(new TimeOnly(hour, minute));

        private static VitalSignRecordResponse Vital(int hour, int minute, int? hr = null, int? sys = null, int? dia = null, int? spo2 = null, decimal? temp = null)
        {
            var entity = VitalSignRecord.Create(new VitalSignRecordCommand
            {
                Date = BaseDate.ToDateTime(TimeOnly.MinValue),
                Time = new TimeSpan(hour, minute, 0),
                HeartRate = hr,
                SystolicBloodPressure = sys,
                DiastolicBloodPressure = dia,
                Spo2 = spo2,
                Temperature = temp
            });

            return VitalSignRecordResponse.ToResponse(entity);
        }

        private static ClinicalEventResponse ClinicalEvent(int hour, int minute, ClinicalEventTypeEnum type, string? observations = null) =>
            ClinicalEventRaw(BaseDate.ToDateTime(TimeOnly.MinValue), new TimeSpan(hour, minute, 0), type, observations);

        private static ClinicalEventResponse ClinicalEventRaw(DateTime date, TimeSpan time, ClinicalEventTypeEnum type, string? observations = null)
        {
            var entity = Domain.Entities.ClinicalEvent.Create(new ClinicalEventCommand
            {
                Date = date,
                Time = time,
                EventType = type,
                Observations = observations
            });

            return ClinicalEventResponse.ToResponse(entity);
        }

        private static PatientPositionResponse Position(int hour, int minute, SurgicalPositionEnum position)
        {
            var entity = PatientPosition.Create(new PatientPositionCommand
            {
                Date = BaseDate.ToDateTime(TimeOnly.MinValue),
                Time = new TimeSpan(hour, minute, 0),
                Position = position
            });

            return PatientPositionResponse.ToResponse(entity);
        }

        [Fact]
        public void Build_Should_Return_No_Data_When_Monitoring_Is_Null()
        {
            var chart = MonitoringChartBuilder.Build(null, NullLogger.Instance);

            Assert.False(chart.HasData);
            Assert.Empty(chart.Rows);
        }

        [Fact]
        public void Build_Should_Return_No_Data_When_Monitoring_Has_No_Timestamps()
        {
            var monitoring = new MonitoringRecordResponse();

            var chart = MonitoringChartBuilder.Build(monitoring, NullLogger.Instance);

            Assert.False(chart.HasData);
            Assert.Empty(chart.Rows);
        }

        [Fact]
        public void Build_With_Few_Events_Should_Produce_A_Single_Simple_Row()
        {
            var monitoring = new MonitoringRecordResponse
            {
                StartedAt = OnBaseDate(8, 0).ToUniversalTime(),
                SurgeryStartedAt = OnBaseDate(8, 15).ToUniversalTime(),
                SurgeryEndedAt = OnBaseDate(9, 0).ToUniversalTime(),
                EndedAt = OnBaseDate(9, 10).ToUniversalTime(),
                VitalSigns = [Vital(8, 30, hr: 80, sys: 120, dia: 80, spo2: 98, temp: 36.5m)]
            };

            var chart = MonitoringChartBuilder.Build(monitoring, NullLogger.Instance);

            Assert.True(chart.HasData);
            Assert.Single(chart.Rows);

            var row = chart.Rows[0];
            Assert.Equal(4, row.TemporalMarkers.Count);
            Assert.Equal(5, row.VitalPoints.Count); // FC, PAS, PAD, SpO2, Temp (sem PAM neste caso)
            Assert.False(row.HasLaneMarkers);
        }

        [Fact]
        public void Build_Long_Surgery_Should_Split_Into_Multiple_Chronological_Rows()
        {
            var monitoring = new MonitoringRecordResponse
            {
                StartedAt = OnBaseDate(8, 0).ToUniversalTime(),
                SurgeryStartedAt = OnBaseDate(8, 10).ToUniversalTime(),
                SurgeryEndedAt = OnBaseDate(13, 30).ToUniversalTime(),
                EndedAt = OnBaseDate(13, 40).ToUniversalTime(),
                VitalSigns =
                [
                    Vital(8, 30, hr: 75, sys: 110, dia: 70, spo2: 99),
                    Vital(10, 15, hr: 82, sys: 118, dia: 76, spo2: 98),
                    Vital(12, 45, hr: 90, sys: 130, dia: 84, spo2: 97)
                ]
            };

            var chart = MonitoringChartBuilder.Build(monitoring, NullLogger.Instance);

            Assert.True(chart.HasData);
            Assert.True(chart.Rows.Count >= 3, "uma cirurgia de ~5h40 (janelas de 2h) deve gerar pelo menos 3 blocos");

            var totalVitalPointsAcrossRows = chart.Rows.Sum(r => r.VitalPoints.Count);
            Assert.True(totalVitalPointsAcrossRows > 0);

            // Nenhum registro deve "desaparecer" na quebra: cada vital tem até 4 séries preenchidas (hr,sys,dia,spo2).
            Assert.Equal(12, totalVitalPointsAcrossRows);
        }

        [Fact]
        public void Build_Should_Classify_Clinical_Events_Into_The_Expected_Lanes()
        {
            var monitoring = new MonitoringRecordResponse
            {
                StartedAt = OnBaseDate(8, 0).ToUniversalTime(),
                SurgeryStartedAt = OnBaseDate(8, 10).ToUniversalTime(),
                SurgeryEndedAt = OnBaseDate(9, 30).ToUniversalTime(),
                ClinicalEvents =
                [
                    ClinicalEvent(8, 5, ClinicalEventTypeEnum.Intubation),
                    ClinicalEvent(8, 20, ClinicalEventTypeEnum.Incision),
                    ClinicalEvent(9, 0, ClinicalEventTypeEnum.Complication, "Hipotensão transitória"),
                ],
                Positions = [Position(8, 12, SurgicalPositionEnum.Supine)]
            };

            var chart = MonitoringChartBuilder.Build(monitoring, NullLogger.Instance);
            var row = Assert.Single(chart.Rows);

            Assert.Single(row.AirwayMarkers);
            Assert.Single(row.SurgicalMarkers);
            Assert.Single(row.ClinicalMarkers);
            Assert.Single(row.PositionMarkers);
            Assert.Contains("Hipotensão transitória", row.ClinicalMarkers[0].Description);
            Assert.True(row.HasLaneMarkers);
        }

        [Fact]
        public void Build_Should_Offset_Events_That_Happen_Close_Together_To_Avoid_Overlap()
        {
            var monitoring = new MonitoringRecordResponse
            {
                StartedAt = OnBaseDate(8, 0).ToUniversalTime(),
                SurgeryStartedAt = OnBaseDate(8, 5).ToUniversalTime(),
                SurgeryEndedAt = OnBaseDate(9, 0).ToUniversalTime(),
                ClinicalEvents =
                [
                    ClinicalEvent(8, 30, ClinicalEventTypeEnum.Complication),
                    ClinicalEvent(8, 31, ClinicalEventTypeEnum.Block),
                    ClinicalEvent(8, 32, ClinicalEventTypeEnum.Other),
                ]
            };

            var chart = MonitoringChartBuilder.Build(monitoring, NullLogger.Instance);
            var row = Assert.Single(chart.Rows);

            Assert.Equal(3, row.ClinicalMarkers.Count);
            var stackLevels = row.ClinicalMarkers.Select(m => m.StackLevel).ToList();
            Assert.True(stackLevels.Distinct().Count() > 1, "eventos muito próximos devem receber níveis de empilhamento diferentes");
        }

        [Fact]
        public void Build_Should_Not_Duplicate_Or_Drop_Events_Across_Row_Boundaries()
        {
            var monitoring = new MonitoringRecordResponse
            {
                StartedAt = OnBaseDate(8, 0).ToUniversalTime(),
                SurgeryStartedAt = OnBaseDate(8, 5).ToUniversalTime(),
                SurgeryEndedAt = OnBaseDate(12, 0).ToUniversalTime(),
                ClinicalEvents = Enumerable.Range(0, 20)
                    .Select(i => ClinicalEvent(8 + i / 4, (i % 4) * 15, ClinicalEventTypeEnum.Other))
                    .ToList()
            };

            var chart = MonitoringChartBuilder.Build(monitoring, NullLogger.Instance);

            var totalEvents = chart.Rows.Sum(r => r.ClinicalMarkers.Count);
            Assert.Equal(20, totalEvents);
        }

        [Fact]
        public void Build_Should_Ignore_A_Single_Record_With_A_Corrupted_Date_Instead_Of_Exploding_Row_Count()
        {
            // Reproduz o bug real que travava a impressão: um único registro com Date
            // "zerada" (default(DateTime), equivalente a um dado corrompido/nunca
            // preenchido) não pode arrastar timelineStart para o ano 1 e gerar
            // centenas de milhares de blocos de 2h até timelineEnd (hoje).
            var monitoring = new MonitoringRecordResponse
            {
                StartedAt = OnBaseDate(8, 0).ToUniversalTime(),
                SurgeryStartedAt = OnBaseDate(8, 10).ToUniversalTime(),
                SurgeryEndedAt = OnBaseDate(10, 0).ToUniversalTime(),
                EndedAt = OnBaseDate(10, 10).ToUniversalTime(),
                VitalSigns =
                [
                    Vital(8, 30, hr: 78, sys: 118, dia: 76, spo2: 98),
                    Vital(9, 15, hr: 80, sys: 120, dia: 78, spo2: 97)
                ],
                ClinicalEvents =
                [
                    ClinicalEventRaw(default, new TimeSpan(9, 0, 0), ClinicalEventTypeEnum.Other)
                ]
            };

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var chart = MonitoringChartBuilder.Build(monitoring, NullLogger.Instance);
            stopwatch.Stop();

            Assert.True(stopwatch.ElapsedMilliseconds < 2000,
                $"Build() levou {stopwatch.ElapsedMilliseconds}ms — indício de que um registro com data corrompida voltou a estourar a quantidade de blocos.");
            Assert.True(chart.Rows.Count <= 60, "o gráfico nunca deve gerar mais blocos que o limite de segurança.");

            Assert.Equal(0, chart.Rows.Sum(r => r.ClinicalMarkers.Count));
            Assert.True(chart.Rows.Sum(r => r.VitalPoints.Count) > 0,
                "os sinais vitais normais devem continuar aparecendo mesmo com o outlier descartado.");
        }
    }
}
