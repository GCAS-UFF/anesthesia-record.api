using Microsoft.Extensions.Logging;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Extensions;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Response.Print;

namespace UFF.FichaAnestesica.Infra.Services
{

    public static class MonitoringChartBuilder
    {
        private const double ViewboxWidth = 1000;
        private const double VitalsPlotHeight = 130;

        private const double EdgeInset = 10;
        private const double VitalsScaleMin = 0;
        private const double VitalsScaleMax = 240;


        private static readonly double[] VitalsGridValues = { 0, 40, 80, 120, 160, 200, 240 };
        private const double HeartRateLabelProximity = 30;


        private const double TempScaleMin = 32;
        private const double TempScaleMax = 42;

        private static readonly TimeSpan RowSpan = TimeSpan.FromHours(1);

        private const int MaxRows = 60;
        private static readonly TimeSpan OutlierWindow = TimeSpan.FromHours(72);

        public static MonitoringChartViewModel Build(MonitoringRecordResponse? monitoring, ILogger logger)
        {
            var result = new MonitoringChartViewModel();

            if (monitoring == null)
            {
                logger.LogInformation("[PDF][Chart] Monitorização ausente — gráfico não será gerado.");
                return result;
            }


            var vitals = monitoring.VitalSigns
                .Select(v => (Time: Combine(v.Date, v.Time).ToLocalTime(), Vital: v))
                .OrderBy(x => x.Time)
                .ToList();

            var events = monitoring.ClinicalEvents
                .Select(e => (Time: Combine(e.Date, e.Time).ToLocalTime(), Event: e))
                .OrderBy(x => x.Time)
                .ToList();

            var positions = monitoring.Positions
                .Select(p => (Time: Combine(p.Date, p.Time).ToLocalTime(), Position: p))
                .OrderBy(x => x.Time)
                .ToList();

            logger.LogInformation(
                "[PDF][Chart] Registros carregados: {Vitals} sinais vitais, {Events} eventos, {Positions} posições.",
                vitals.Count, events.Count, positions.Count);

            var anesthesiaStart = IsSet(monitoring.StartedAt) ? monitoring.StartedAt.ToLocalTime() : (DateTime?)null;
            var anesthesiaEnd = monitoring.EndedAt?.ToLocalTime();
            var surgeryStart = IsSet(monitoring.SurgeryStartedAt) ? monitoring.SurgeryStartedAt.ToLocalTime() : (DateTime?)null;
            var surgeryEnd = monitoring.SurgeryEndedAt?.ToLocalTime();
   
            var anchor = surgeryStart ?? anesthesiaStart ?? surgeryEnd ?? anesthesiaEnd;

            if (!anchor.HasValue)
            {
                var rawTimes = vitals.Select(v => v.Time)
                    .Concat(events.Select(e => e.Time))
                    .Concat(positions.Select(p => p.Time))
                    .OrderBy(t => t)
                    .ToList();

                if (rawTimes.Count > 0)
                    anchor = rawTimes[rawTimes.Count / 2];
            }

            if (!anchor.HasValue)
            {
                logger.LogInformation("[PDF][Chart] Nenhum marco temporal e nenhum registro na monitorização — gráfico não será gerado.");
                return result;
            }

            bool WithinWindow(DateTime t) => Math.Abs((t - anchor.Value).TotalHours) <= OutlierWindow.TotalHours;

            var vitalsBefore = vitals.Count;
            var eventsBefore = events.Count;
            var positionsBefore = positions.Count;

            vitals = vitals.Where(v => WithinWindow(v.Time)).ToList();
            events = events.Where(e => WithinWindow(e.Time)).ToList();
            positions = positions.Where(p => WithinWindow(p.Time)).ToList();

            if (anesthesiaStart.HasValue && !WithinWindow(anesthesiaStart.Value)) anesthesiaStart = null;
            if (anesthesiaEnd.HasValue && !WithinWindow(anesthesiaEnd.Value)) anesthesiaEnd = null;
            if (surgeryStart.HasValue && !WithinWindow(surgeryStart.Value)) surgeryStart = null;
            if (surgeryEnd.HasValue && !WithinWindow(surgeryEnd.Value)) surgeryEnd = null;

            var discarded = (vitalsBefore - vitals.Count) + (eventsBefore - events.Count) + (positionsBefore - positions.Count);
            if (discarded > 0)
            {
                logger.LogWarning(
                    "[PDF][Chart] Descartados {Discarded} registro(s) com horário a mais de {Hours}h da referência ({Anchor:O}) — provável dado com data inválida/corrompida. Eles não aparecerão no gráfico.",
                    discarded, OutlierWindow.TotalHours, anchor.Value);
            }

            var allTimes = new List<DateTime>();
            if (anesthesiaStart.HasValue) allTimes.Add(anesthesiaStart.Value);
            if (anesthesiaEnd.HasValue) allTimes.Add(anesthesiaEnd.Value);
            if (surgeryStart.HasValue) allTimes.Add(surgeryStart.Value);
            if (surgeryEnd.HasValue) allTimes.Add(surgeryEnd.Value);
            allTimes.AddRange(vitals.Select(v => v.Time));
            allTimes.AddRange(events.Select(e => e.Time));
            allTimes.AddRange(positions.Select(p => p.Time));

            if (allTimes.Count == 0)
            {
                logger.LogInformation("[PDF][Chart] Nenhum registro dentro da janela válida — gráfico não será gerado.");
                return result;
            }

            var timelineStart = allTimes.Min();
            var timelineEnd = allTimes.Max();

            if (timelineEnd <= timelineStart)
                timelineEnd = timelineStart + TimeSpan.FromHours(1);

            result.HasData = true;

            var rawRowCount = (int)Math.Ceiling((timelineEnd - timelineStart) / RowSpan);

            if (rawRowCount > MaxRows)
            {
                logger.LogWarning(
                    "[PDF][Chart] Linha do tempo calculada ({Hours:0}h, {Rows} blocos) excede o limite de segurança de {Cap} blocos — truncando para {CapHours}h. Isso não deveria acontecer para uma cirurgia real; investigar dados de origem.",
                    (timelineEnd - timelineStart).TotalHours, rawRowCount, MaxRows, MaxRows * RowSpan.TotalHours);

                timelineEnd = timelineStart + TimeSpan.FromTicks(RowSpan.Ticks * MaxRows);
            }

            var rowCount = Math.Clamp(rawRowCount, 1, MaxRows);

            logger.LogInformation(
                "[PDF][Chart] Linha do tempo: {Start:HH:mm} a {End:HH:mm} ({Hours:0.0}h) em {Rows} bloco(s).",
                timelineStart, timelineEnd, (timelineEnd - timelineStart).TotalHours, rowCount);

            for (var i = 0; i < rowCount; i++)
            {
                var rowStart = timelineStart + TimeSpan.FromTicks(RowSpan.Ticks * i);
                var rowEnd = i == rowCount - 1 ? timelineEnd : rowStart + RowSpan;

                if (rowEnd <= rowStart)
                    rowEnd = rowStart + TimeSpan.FromMinutes(1);

                var row = new MonitoringChartRow
                {
                    RangeLabel = $"{rowStart:HH:mm} – {rowEnd:HH:mm}"
                };

                var isLastRow = i == rowCount - 1;

                BuildTicks(row, rowStart, rowEnd);
                BuildValueTicks(row);

                foreach (var (time, vital) in vitals.Where(v => InRange(v.Time, rowStart, rowEnd, isLastRow)))
                {
                    var x = XFor(time, rowStart, rowEnd);
                    AddVitalPoint(row, x, VitalSeries.HeartRate, vital.HeartRate);
                    AddHeartRateLabel(row, x, vital.HeartRate);
                    AddVitalPoint(row, x, VitalSeries.SystolicBp, vital.SystolicBloodPressure);
                    AddVitalPoint(row, x, VitalSeries.DiastolicBp, vital.DiastolicBloodPressure);
                    AddVitalPoint(row, x, VitalSeries.MeanBp, vital.MeanArterialPressure);
                    AddVitalPoint(row, x, VitalSeries.Spo2, vital.Spo2);
                    AddTempPoint(row, x, vital.Temperature);
                }

                AddTemporalMarker(row, anesthesiaStart, rowStart, rowEnd, isLastRow, TemporalMarkerKind.AnesthesiaStart);
                AddTemporalMarker(row, anesthesiaEnd, rowStart, rowEnd, isLastRow, TemporalMarkerKind.AnesthesiaEnd);
                AddTemporalMarker(row, surgeryStart, rowStart, rowEnd, isLastRow, TemporalMarkerKind.SurgeryStart);
                AddTemporalMarker(row, surgeryEnd, rowStart, rowEnd, isLastRow, TemporalMarkerKind.SurgeryEnd);

                var rowEvents = events.Where(e => InRange(e.Time, rowStart, rowEnd, isLastRow));
                foreach (var (time, ev) in rowEvents)
                {
                    var lane = ClassifyLane(ev.EventType);
                    var description = string.IsNullOrWhiteSpace(ev.CatalogEventName) ? ev.EventType.SafeDescription() : ev.CatalogEventName;
                    if (!string.IsNullOrWhiteSpace(ev.Observations))
                        description += $" — {ev.Observations}";

                    var marker = new LaneChartMarker
                    {
                        X = XFor(time, rowStart, rowEnd),
                        TimeLabel = time.ToString("HH:mm"),
                        Description = description
                    };

                    GetLane(row, lane).Add(marker);
                }

                foreach (var (time, position) in positions.Where(p => InRange(p.Time, rowStart, rowEnd, isLastRow)))
                {
                    row.PositionMarkers.Add(new LaneChartMarker
                    {
                        X = XFor(time, rowStart, rowEnd),
                        TimeLabel = time.ToString("HH:mm"),
                        Description = position.Position.SafeDescription()
                    });
                }

                ApplyAntiOverlap(row.SurgicalMarkers);
                ApplyAntiOverlap(row.AirwayMarkers);
                ApplyAntiOverlap(row.ClinicalMarkers);
                ApplyAntiOverlap(row.PositionMarkers);
                ApplyAntiOverlap(row.HeartRateLabels, HeartRateLabelProximity);

                result.Rows.Add(row);
            }

            return result;
        }

        private static DateTime Combine(DateTime date, TimeSpan time) => date.Date + time;

        private static bool IsSet(DateTime value) => value != default;
    
        private static bool InRange(DateTime time, DateTime rowStart, DateTime rowEnd, bool isLastRow) =>
            time >= rowStart && (isLastRow ? time <= rowEnd : time < rowEnd);

        private static double XFor(DateTime time, DateTime rowStart, DateTime rowEnd)
        {
            var span = (rowEnd - rowStart).TotalMinutes;
            if (span <= 0) return EdgeInset;
            var pct = (time - rowStart).TotalMinutes / span;
            var x = Math.Clamp(pct, 0, 1) * ViewboxWidth;
            return Math.Clamp(x, EdgeInset, ViewboxWidth - EdgeInset);
        }

        private static void BuildTicks(MonitoringChartRow row, DateTime rowStart, DateTime rowEnd)
        {
            var totalMinutes = (rowEnd - rowStart).TotalMinutes;
            var step = totalMinutes > 90 ? 15 : totalMinutes > 40 ? 10 : totalMinutes > 20 ? 5 : 2;
         
            var index = 0;
            for (var minute = 0; minute <= totalMinutes; minute += step, index++)
            {
                var tickTime = rowStart.AddMinutes(minute);
                row.Ticks.Add(new ChartAxisTick
                {
                    X = XFor(tickTime, rowStart, rowEnd),
                    Label = tickTime.ToString("HH:mm"),
                    IsMajor = index % 2 == 0
                });
            }
        }

        private static void BuildValueTicks(MonitoringChartRow row)
        {
            foreach (var value in VitalsGridValues)
            {
                row.ValueTicks.Add(new ChartValueTick
                {
                    Y = YForVitalsScale(value),
                    Value = value,
                    Label = ((int)value).ToString()
                });
            }
        }

        private static double YForVitalsScale(double value)
        {
            var clamped = Math.Clamp(value, VitalsScaleMin, VitalsScaleMax);
            var pct = (clamped - VitalsScaleMin) / (VitalsScaleMax - VitalsScaleMin);
            return VitalsPlotHeight - (pct * VitalsPlotHeight);
        }

        private static double YForTempScale(double value)
        {
            var clamped = Math.Clamp(value, TempScaleMin, TempScaleMax);
            var pct = (clamped - TempScaleMin) / (TempScaleMax - TempScaleMin);
            return VitalsPlotHeight - (pct * VitalsPlotHeight);
        }

        private static void AddVitalPoint(MonitoringChartRow row, double x, VitalSeries series, int? value)
        {
            if (!value.HasValue) return;
            row.VitalPoints.Add(new VitalChartPoint { X = x, Y = YForVitalsScale(value.Value), Series = series });
        }

        private static void AddTempPoint(MonitoringChartRow row, double x, decimal? value)
        {
            if (!value.HasValue) return;
            row.VitalPoints.Add(new VitalChartPoint { X = x, Y = YForTempScale((double)value.Value), Series = VitalSeries.Temperature });
        }

        private static void AddHeartRateLabel(MonitoringChartRow row, double x, int? value)
        {
            if (!value.HasValue) return;
            row.HeartRateLabels.Add(new HeartRateLabelMarker { X = x, Y = YForVitalsScale(value.Value), Value = value.Value });
        }

        private static void AddTemporalMarker(MonitoringChartRow row, DateTime? time, DateTime rowStart, DateTime rowEnd, bool isLastRow, TemporalMarkerKind kind)
        {
            if (!time.HasValue || !InRange(time.Value, rowStart, rowEnd, isLastRow)) return;

            row.TemporalMarkers.Add(new TemporalChartMarker
            {
                X = XFor(time.Value, rowStart, rowEnd),
                Kind = kind,
                TimeLabel = time.Value.ToString("HH:mm")
            });
        }

        private enum EventLaneKind { Surgical, Airway, Clinical, Position }

        private static EventLaneKind ClassifyLane(ClinicalEventTypeEnum type) => type switch
        {
            ClinicalEventTypeEnum.Intubation => EventLaneKind.Airway,
            ClinicalEventTypeEnum.Extubation => EventLaneKind.Airway,
            ClinicalEventTypeEnum.Incision => EventLaneKind.Surgical,
            ClinicalEventTypeEnum.TourniquetOn => EventLaneKind.Surgical,
            ClinicalEventTypeEnum.TourniquetOff => EventLaneKind.Surgical,
            ClinicalEventTypeEnum.Position => EventLaneKind.Position,
            ClinicalEventTypeEnum.Block => EventLaneKind.Clinical,
            ClinicalEventTypeEnum.Complication => EventLaneKind.Clinical,
            _ => EventLaneKind.Clinical
        };

        private static List<LaneChartMarker> GetLane(MonitoringChartRow row, EventLaneKind lane) => lane switch
        {
            EventLaneKind.Surgical => row.SurgicalMarkers,
            EventLaneKind.Airway => row.AirwayMarkers,
            EventLaneKind.Position => row.PositionMarkers,
            _ => row.ClinicalMarkers
        };

        private static void ApplyAntiOverlap<T>(List<T> markers, double proximityThreshold = 35) where T : IStackableMarker
        {
            markers.Sort((a, b) => a.X.CompareTo(b.X));

            var lastX = double.NegativeInfinity;
            var level = 0;

            foreach (var marker in markers)
            {
                level = marker.X - lastX < proximityThreshold ? (level + 1) % 3 : 0;
                marker.StackLevel = level;
                lastX = marker.X;
            }
        }
    }
}
