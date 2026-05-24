using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class SurgeryRepository : ISurgeryRepository
    {
        private readonly SigaDbCtx _context;

        public SurgeryRepository(SigaDbCtx context)
        {
            _context = context;
        }

        public async Task AddOrUpdatePatientsAsync(IList<Patient> patients)
        {
            var patientIds = patients.Select(p => p.PatientId).ToList();

            var existingPatients = await _context.Patients
                .AsSplitQuery()
                .Include(p => p.CurrentLocation)
                    .ThenInclude(cl => cl.Unit)
                .Include(p => p.Surgeries)
                    .ThenInclude(s => s.Specialty)
                .Include(p => p.Surgeries)
                    .ThenInclude(s => s.Location)
                        .ThenInclude(l => l.SurgicalCenter)
                .Include(p => p.Surgeries)
                    .ThenInclude(s => s.Procedures)
                .Where(p => patientIds.Contains(p.PatientId))
                .ToListAsync();

            var existingPatientsDict = existingPatients.ToDictionary(p => p.PatientId);
            var unitsDict = await _context.Units.ToDictionaryAsync(x => x.Code);
            var specialtiesDict = await _context.Specialties.ToDictionaryAsync(x => x.Code);
            var centersDict = await _context.SurgicalCenters.ToDictionaryAsync(x => x.Code);
            var proceduresDict = await _context.Procedures.ToDictionaryAsync(x => x.ExternalId);

            foreach (var patient in patients)
            {
                ResolveOrCreateRelatedEntities(patient, unitsDict, specialtiesDict, centersDict, proceduresDict);

                if (!existingPatientsDict.TryGetValue(patient.PatientId, out var existingPatient))
                {
                    await _context.Patients.AddAsync(patient);
                }
                else
                {
                    UpdateExistingPatient(existingPatient, patient);
                }
            }

            await _context.SaveChangesAsync();
        }

        private void ResolveOrCreateRelatedEntities(Patient patient, Dictionary<string, Unit> unitsDict, Dictionary<string, Specialty> specialtiesDict,
            Dictionary<string, SurgicalCenter> centersDict, Dictionary<string, Procedure> proceduresDict)
        {
            if (patient.CurrentLocation?.Unit != null)
            {
                var code = patient.CurrentLocation.Unit.Code;
                if (unitsDict.TryGetValue(code, out var existingUnit))
                {
                    patient.CurrentLocation.SetUnit(existingUnit);
                }
                else
                {
                    _context.Units.Add(patient.CurrentLocation.Unit);
                    unitsDict[code] = patient.CurrentLocation.Unit;
                }
            }

            foreach (var surgery in patient.Surgeries)
            {
                if (surgery.Specialty != null)
                {
                    var code = surgery.Specialty.Code;
                    if (specialtiesDict.TryGetValue(code, out var existingSpecialty))
                    {
                        surgery.SetSpecialty(existingSpecialty);
                    }
                    else
                    {
                        _context.Specialties.Add(surgery.Specialty);
                        specialtiesDict[code] = surgery.Specialty;
                    }
                }

                if (surgery.Location?.SurgicalCenter != null)
                {
                    var code = surgery.Location.SurgicalCenter.Code;
                    if (centersDict.TryGetValue(code, out var existingCenter))
                    {
                        surgery.Location.SetSurgicalCenter(existingCenter);
                    }
                    else
                    {
                        _context.SurgicalCenters.Add(surgery.Location.SurgicalCenter);
                        centersDict[code] = surgery.Location.SurgicalCenter;
                    }
                }

                var resolvedProcedures = new List<Procedure>();
                foreach (var procedure in surgery.Procedures)
                {
                    if (proceduresDict.TryGetValue(procedure.ExternalId, out var existingProcedure))
                    {

                        existingProcedure.Update(procedure.Description, procedure.Cid, procedure.IsPrimary);
                        resolvedProcedures.Add(existingProcedure);
                    }
                    else
                    {
                        _context.Procedures.Add(procedure);
                        proceduresDict[procedure.ExternalId] = procedure;
                        resolvedProcedures.Add(procedure);
                    }
                }
                surgery.ReplaceProcedures(resolvedProcedures);
            }
        }

        private void UpdateExistingPatient(Patient existingPatient, Patient newPatientData)
        {
            existingPatient.UpdatePatient(newPatientData);

            if (newPatientData.CurrentLocation != null)
            {
                if (existingPatient.CurrentLocation == null)
                    existingPatient.SetCurrentLocation(CurrentLocation.Update(newPatientData.CurrentLocation));
            }

            existingPatient.Surgeries.Clear();
            foreach (var surgery in newPatientData.Surgeries)
            {
                existingPatient.Surgeries.Add(surgery);
            }
        }
        public async Task<List<Patient>> GetPatientsWithSurgeriesAsync(DateTime? date = null, SurgeryStatus? status = null, int page = 1, int size = 10)
        {
            var query = _context.Patients
                .AsSplitQuery()
                .Include(p => p.CurrentLocation)
                    .ThenInclude(cl => cl.Unit)
                .Include(p => p.Surgeries)
                    .ThenInclude(s => s.Specialty)
                .Include(p => p.Surgeries)
                    .ThenInclude(s => s.Location)
                        .ThenInclude(l => l.SurgicalCenter)
                .Include(p => p.Surgeries)
                    .ThenInclude(s => s.Procedures)
                .AsQueryable();

            if (date.HasValue)
            {
                query = query.Where(p =>
                    p.Surgeries.Any(s => s.SurgeryDate.Date == date.Value.Date));
            }

            if (status.HasValue)
            {
                query = query.Where(p =>
                    p.Surgeries.Any(s => s.Status == status.Value));
            }

            return await query
             .Skip((page - 1) * size)
             .Take(size)
             .ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _context.Patients
                .Include(p => p.CurrentLocation)
                    .ThenInclude(cl => cl.Unit)
                .Include(p => p.Surgeries)
                    .ThenInclude(s => s.Specialty)
                .Include(p => p.Surgeries)
                    .ThenInclude(s => s.Location)
                        .ThenInclude(l => l.SurgicalCenter)
                .Include(p => p.Surgeries)
                    .ThenInclude(s => s.Procedures)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}