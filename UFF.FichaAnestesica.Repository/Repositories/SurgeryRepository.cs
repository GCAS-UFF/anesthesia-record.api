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

            var existingDict = existingPatients.ToDictionary(p => p.PatientId);
            var proceduresDict = await _context.Procedures.ToDictionaryAsync(p => p.ExternalId);
            var specialtiesDict = await _context.Specialties.ToDictionaryAsync(s => s.Code);
            var unitsDict = await _context.Units.ToDictionaryAsync(u => u.Code);
            var centersDict = await _context.SurgicalCenters.ToDictionaryAsync(sc => sc.Code);


            var allSurgeryIds = patients
                    .SelectMany(p => p.Surgeries)
                    .Select(s => s.SurgeryId)
                    .Where(id => !string.IsNullOrEmpty(id))
                    .Distinct()
                    .ToList();

            var existingSurgeries = await _context.Surgeries
                    .Include(s => s.Procedures)
                    .Include(s => s.Specialty)
                    .Include(s => s.Location)
                        .ThenInclude(l => l.SurgicalCenter)
                    .Where(s => allSurgeryIds.Contains(s.SurgeryId))
                    .ToListAsync();

            var surgeriesDict = existingSurgeries
                    .ToDictionary(s => s.SurgeryId);

            foreach (var patient in patients)
            {
                ResolveReferences(patient, proceduresDict, specialtiesDict, unitsDict, centersDict);
                ResolveSurgeries(patient, surgeriesDict); // 🔥 NOVO

                var existing = existingDict.GetValueOrDefault(patient.PatientId);

                if (existing == null)
                {
                    await _context.Patients.AddAsync(patient);
                    continue;
                }

                existing.Sync(patient);
            }

            await _context.SaveChangesAsync();
        }

        private void ResolveSurgeries(Patient patient, Dictionary<string, Surgery> surgeriesDict)
        {
            var resolved = new List<Surgery>();

            foreach (var surgery in patient.Surgeries)
            {
                if (surgeriesDict.TryGetValue(surgery.SurgeryId, out var existing))
                {
                    // 🔥 ATUALIZA a entidade rastreada
                    existing.Sync(surgery);

                    resolved.Add(existing);
                }
                else
                {
                    resolved.Add(surgery);
                }
            }

            patient.ReplaceSurgeries(resolved); // 🔥 ESSENCIAL
        }

        private void ResolveReferences(Patient patient, Dictionary<string, Procedure> proceduresDict, Dictionary<string, Specialty> specialtiesDict, Dictionary<string, Unit> unitsDict, Dictionary<string, SurgicalCenter> centersDict)
        {

            if (patient.CurrentLocation?.Unit != null)
            {
                var code = patient.CurrentLocation.Unit.Code;

                if (unitsDict.TryGetValue(code, out var existing))
                {
                    patient.CurrentLocation.SyncUnit(existing);
                }
                else
                {
                    unitsDict[code] = patient.CurrentLocation.Unit;
                    _context.Units.Add(patient.CurrentLocation.Unit);
                }
            }

            foreach (var surgery in patient.Surgeries)
            {
                if (surgery.Specialty != null)
                {
                    var code = surgery.Specialty.Code;

                    if (specialtiesDict.TryGetValue(code, out var existing))
                    {
                        surgery.SetSpecialty(existing);
                    }
                    else
                    {
                        specialtiesDict[code] = surgery.Specialty;
                        _context.Specialties.Add(surgery.Specialty);
                    }
                }

                if (surgery.Location?.SurgicalCenter != null)
                {
                    var code = surgery.Location.SurgicalCenter.Code;

                    if (centersDict.TryGetValue(code, out var existing))
                    {
                        surgery.Location.SetSurgicalCenter(existing);
                    }
                    else
                    {
                        centersDict[code] = surgery.Location.SurgicalCenter;
                        _context.SurgicalCenters.Add(surgery.Location.SurgicalCenter);
                    }
                }

                var resolved = new List<Procedure>();

                foreach (var proc in surgery.Procedures)
                {
                    if (proceduresDict.TryGetValue(proc.ExternalId, out var existing))
                    {
                        existing.Sync(proc);
                        resolved.Add(existing);
                    }
                    else
                    {
                        proceduresDict[proc.ExternalId] = proc;
                        resolved.Add(proc);
                    }
                }

                surgery.ReplaceProcedures(resolved);
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

            return await  query
             .Skip((page - 1) * size)
             .Take(size)
             .ToListAsync();
        }
    }
}