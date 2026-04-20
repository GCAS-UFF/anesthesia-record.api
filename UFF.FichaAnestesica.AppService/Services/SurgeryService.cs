using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Service.Services
{
    public class SurgeryService : ISurgeryService
    {     
        public async Task<IEnumerable<SurgeryListDto>> GetSurgeriesAsync(DateTime date, SurgeryStatus status)
        {
            return null;

            //var client = _httpClientFactory.CreateClient();
            //var response = await client.GetAsync("http://127.0.0.1:8000/api/cirurgias/hoje");

            //if (!response.IsSuccessStatusCode)
            //{
            //    throw new Exception("Error fetching data from HUAP.");
            //}

            //var content = await response.Content.ReadAsStringAsync();
            //using var jsonDoc = JsonDocument.Parse(content);
            //var root = jsonDoc.RootElement;
            
            //var mappedList = new List<SurgeryListDto>();

            //if (root.TryGetProperty("pacientes", out var patientsElement))
            //{
            //    foreach (var patientElement in patientsElement.EnumerateArray())
            //    {
            //        // Map Patient
            //        var medicalRecord = patientElement.GetProperty("numero_prontuario").GetString() ?? string.Empty;
            //        var name = patientElement.GetProperty("nome_completo").GetString() ?? string.Empty;
            //        var birthDateStr = patientElement.GetProperty("data_nascimento").GetString() ?? string.Empty;
                    
            //        var externalIdPatient = patientElement.TryGetProperty("id", out var pId) ? pId.GetString() ?? "" : "";
            //        var gender = patientElement.TryGetProperty("sexo", out var g) ? g.GetString() ?? "Não Informado" : "Não Informado";
                    
            //        float? weight = null;
            //        if (patientElement.TryGetProperty("peso_kg", out var w) && w.ValueKind == JsonValueKind.Number)
            //            weight = (float)w.GetDouble();
                        
            //        float? height = null;
            //        if (patientElement.TryGetProperty("altura_cm", out var h) && h.ValueKind == JsonValueKind.Number)
            //            height = (float)h.GetDouble();

            //        DateTime.TryParse(birthDateStr, out DateTime birthDate);

            //        // EF Core Upsert Logic for Patient: Check if exists to avoid duplication
            //        var patient = await _context.Patient
            //            .FirstOrDefaultAsync(p => p.MedicalRecord == medicalRecord);
                        
            //        if (patient == null) 
            //        { 
            //            patient = new Patient 
            //            { 
            //                Id = Guid.NewGuid(), 
            //                ExternalIdHuap = externalIdPatient,
            //                MedicalRecord = medicalRecord,
            //                Name = name,
            //                BirthDate = birthDate.ToUniversalTime(),
            //                Gender = gender,
            //                WeightKg = weight,
            //                HeightCm = height
            //            };
            //            _context.Patient.Add(patient);
            //        }
            //        else 
            //        {
            //            patient.ExternalIdHuap = externalIdPatient;
            //            patient.Name = name;
            //            patient.BirthDate = birthDate.ToUniversalTime();
            //            patient.Gender = gender;
            //            patient.WeightKg = weight;
            //            patient.HeightCm = height;
            //            _context.Patient.Update(patient);
            //        }
            //        // Save immediately to get Patient ID for Foreign Key later
            //        await _context.SaveChangesAsync();

            //        if (patientElement.TryGetProperty("cirurgias", out var surgeriesElement))
            //        {
            //            foreach (var surgeryElement in surgeriesElement.EnumerateArray())
            //            {
            //                // Map Surgery (SurgicalCase)
            //                var externalId = surgeryElement.GetProperty("id").ToString();
            //                var statusPhp = surgeryElement.GetProperty("status_cirurgia").GetString() ?? "espera";
            //                var mappedStatus = statusPhp.ToLower() == "agendada" ? "waiting" : statusPhp;
            //                var room = surgeryElement.GetProperty("local").GetProperty("sala").GetString() ?? "";
                            
            //                var bed = "";
            //                if (patientElement.TryGetProperty("localizacao_atual", out var localAtual) && 
            //                    localAtual.TryGetProperty("leito", out var leitoProp))
            //                {
            //                    bed = leitoProp.GetString() ?? "";
            //                }
                            
            //                var surgeon = "";
            //                var specialty = "";
            //                if (surgeryElement.TryGetProperty("equipe_medica", out var team))
            //                {
            //                    surgeon = team.GetProperty("cirurgiao_principal").GetString() ?? "";
            //                    specialty = team.GetProperty("cirurgiao_especialidade").GetString() ?? "";
            //                }
                            
            //                string procedure = "";
            //                if (surgeryElement.TryGetProperty("procedimentos", out var procedures))
            //                {
            //                    foreach (var proc in procedures.EnumerateArray())
            //                    {
            //                        if (proc.TryGetProperty("principal", out var isPrincipal) && isPrincipal.GetBoolean())
            //                        {
            //                            procedure = proc.GetProperty("descricao").GetString() ?? "";
            //                            break;
            //                        }
            //                    }
            //                }

            //                // EF Core Upsert Logic for SurgicalCase based on External API ID
            //                var surgicalCase = await _context.SurgicalCase
            //                    .FirstOrDefaultAsync(s => s.ExternalIdHuap == externalId);
                                
            //                if (surgicalCase == null)
            //                {
            //                    surgicalCase = new SurgicalCase 
            //                    { 
            //                        Id = Guid.NewGuid(), 
            //                        ExternalIdHuap = externalId,
            //                        PatientId = patient.Id,
            //                        ProposedProcedure = procedure,
            //                        Room = room,
            //                        Bed = bed,
            //                        Status = mappedStatus,
            //                        Surgeon = surgeon,
            //                        Specialty = specialty,
            //                        SurgeryDate = DateTime.UtcNow.Date
            //                    };
            //                    _context.SurgicalCase.Add(surgicalCase);
            //                }
            //                else 
            //                {
            //                    surgicalCase.ProposedProcedure = procedure;
            //                    surgicalCase.Room = room;
            //                    surgicalCase.Bed = bed;
            //                    surgicalCase.Status = mappedStatus;
            //                    surgicalCase.Surgeon = surgeon;
            //                    surgicalCase.Specialty = specialty;
            //                    surgicalCase.SurgeryDate = DateTime.UtcNow.Date;
            //                    _context.SurgicalCase.Update(surgicalCase);
            //                }

            //                // Push to Frontend view list
            //                mappedList.Add(new SurgeryListDto
            //                {
            //                    Id = surgicalCase.Id,
            //                    Name = patient.Name,
            //                    MedicalRecord = patient.MedicalRecord,
            //                    BirthDate = birthDateStr,
            //                    Room = surgicalCase.Room,
            //                    Procedure = surgicalCase.ProposedProcedure,
            //                    Status = surgicalCase.Status
            //                });
            //            }
            //        }
            //    }
            //}

            //await _context.SaveChangesAsync();
            //return mappedList;
        }
    }
}


