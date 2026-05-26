//using UFF.FichaAnestesica.Domain.Dto;
//using UFF.FichaAnestesica.Domain.Entities;
//using UFF.FichaAnestesica.Domain.Enums;
//using UFF.FichaAnestesica.Domain.Extensions;

//namespace UFF.FichaAnestesica.Service.Mappers
//{
//    public static class PatientMapper
//    {
//        public static List<Patient> Map(IEnumerable<PatientDto> viewData)
//        {
//            var patients = new Dictionary<string, Patient>();

//            var units = new Dictionary<string, Unit>();
//            var specialties = new Dictionary<string, Specialty>();
//            var centers = new Dictionary<string, SurgicalCenter>();

//            foreach (var item in viewData)
//            {
//                if (string.IsNullOrWhiteSpace(item.Id))
//                    continue;

//                if (!patients.TryGetValue(item.Id, out var patient))
//                {
//                    patient = CreatePatient(item, units);

//                    patients[item.Id] = patient;
//                }

//                MapSurgery(patient, item, specialties, centers);
//            }

//            return patients.Values.ToList();
//        }

//        private static SurgicalCenter? GetOrCreateSurgicalCenter(PatientDto item, Dictionary<string, SurgicalCenter> cache)
//        {
//            if (string.IsNullOrWhiteSpace(item.SurgicalCenterCode))
//                return null;

//            if (cache.TryGetValue(item.SurgicalCenterCode, out var center))
//                return center;

//            center = SurgicalCenter.Create(
//                item.SurgicalCenterCode,
//                item.SurgicalCenterDescription);

//            cache[item.SurgicalCenterCode] = center;

//            return center;
//        }

//        private static Specialty? GetOrCreateSpecialty(PatientDto item, Dictionary<string, Specialty> cache)
//        {
//            if (string.IsNullOrWhiteSpace(item.SpecialtyCode))
//                return null;

//            if (cache.TryGetValue(item.SpecialtyCode, out var specialty))
//                return specialty;

//            specialty = Specialty.Create(
//                item.SpecialtyCode,
//                item.SpecialtyDescription);

//            cache[item.SpecialtyCode] = specialty;
//            return specialty;
//        }

//        private static void MapSurgery(Patient patient, PatientDto item, Dictionary<string, Specialty> specialties, Dictionary<string, SurgicalCenter> surgicalCenters)
//        {
//            if (string.IsNullOrWhiteSpace(item.SurgeryId))
//                return;

//            var surgery = patient.Surgeries
//                .FirstOrDefault(s => s.SurgeryId == item.SurgeryId);

//            if (surgery == null)
//            {
//                var specialty = GetOrCreateSpecialty(item, specialties);

//                var surgicalCenter = GetOrCreateSurgicalCenter(
//                    item,
//                    surgicalCenters
//                );

//                var location = SurgeryLocation.Create(
//                    item.SurgeryRoom,
//                    surgicalCenter
//                );

//                surgery = Surgery.Create(
//                    item.SurgeryId,
//                    item.SurgeryDate,
//                    item.SurgeryStatus.ToSurgeryStatus(),
//                    patient.PatientId,
//                    specialty,
//                    location
//                );

//                patient.SyncSurgery(surgery);
//            }

//            MapProcedure(surgery, item);
//        }

//        private static void MapProcedure(Surgery surgery, PatientDto item)
//        {
//            if (string.IsNullOrWhiteSpace(item.ProcedureId))
//                return;

//            var exists = surgery.Procedures.Any(p => p.ExternalId == item.ProcedureId);

//            if (exists)
//                return;

//            var procedure = Procedure.Create(
//                item.ProcedureId,
//                item.ProcedureDescription,
//                item.ProcedureCid,
//                item.IsPrimaryProcedure);

//            surgery.AddProcedure(procedure);
//        }      

//        private static Patient CreatePatient(PatientDto item, Dictionary<string, Unit> units)
//        {
//            var unit = GetOrCreateUnit(item, units);

//            var location = CurrentLocation.Create(
//                item.Bed,
//                item.Floor,
//                item.Room,
//                unit
//            );

//            var patient = Patient.Create(
//                item.Id,
//                item.MedicalRecordNumber,
//                item.FullName,
//                item.BirthDate,
//                item.Gender == "M"
//                    ? GenderEnum.Male
//                    : GenderEnum.Female,
//                item.WeightKg,
//                item.HeightCm,
//                location
//            );

//            patient.UpdateExternalCode(item.Id);

//            return patient;
//        }

//        private static Unit? GetOrCreateUnit(PatientDto item, Dictionary<string, Unit> cache)
//        {
//            if (string.IsNullOrWhiteSpace(item.UnitCode))
//                return null;

//            if (cache.TryGetValue(item.UnitCode, out var unit))
//                return unit;

//            unit = Unit.Create(
//                item.UnitCode,
//                item.UnitDescription
//            );

//            cache[item.UnitCode] = unit;

//            return unit;
//        }
//    }
//}