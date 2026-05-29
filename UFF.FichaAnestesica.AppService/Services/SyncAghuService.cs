using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Service.Services
{
    public class SyncAghuService : ISyncAghuService
    {
        private readonly IProfessionalReadOnlyRepository _professionalReadOnlyRepository;
        private readonly IMedicineReadOnlyRepository _medicineApiReadOnlyRepository;

        public SyncAghuService(IProfessionalReadOnlyRepository professionalReadOnlyRepository, IMedicineReadOnlyRepository medicineApiReadOnlyRepository)
        {
            _professionalReadOnlyRepository = professionalReadOnlyRepository;
            _medicineApiReadOnlyRepository = medicineApiReadOnlyRepository;
        }      

        public async Task Sync()
        {            
        }       
    }
}