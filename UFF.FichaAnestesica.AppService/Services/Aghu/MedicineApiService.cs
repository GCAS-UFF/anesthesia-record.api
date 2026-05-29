using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Extensions;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class MedicineService : IMedicineApiService
    {
        private readonly IMedicineApiReadOnlyRepository _medicineApiReadOnlyRepository;


        private readonly IDrugRepository _drugRepository;

        public MedicineService(IMedicineApiReadOnlyRepository medicineApiReadOnlyRepository, IDrugRepository drugRepository)
        {
            _medicineApiReadOnlyRepository = medicineApiReadOnlyRepository;
            _drugRepository = drugRepository;
        }

        public async Task SyncMedicines()
        {
            var drugs = await _medicineApiReadOnlyRepository.GetDrugssFromAGHU();

            if (drugs == null || !drugs.Any())
                return;

            var dbDrugs = await _drugRepository.GetAllAsync();
            var drugsByExternalId = dbDrugs.Where(x => !string.IsNullOrWhiteSpace(x.ExternalId)).ToDictionary(x => x.ExternalId);
            var externalIds = new HashSet<string>();

            foreach (var medicine in drugs)
            {
                if (string.IsNullOrWhiteSpace(medicine.Id))
                    continue;

                externalIds.Add(medicine.Id);

                var presentation = ParsePresentationsExtensions.ParseToEnum(medicine.Presentation);

                if (drugsByExternalId.TryGetValue(medicine.Id, out var existingDrug))
                {
                    existingDrug.Update(medicine.Description, presentation);
                    _drugRepository.Update(existingDrug);
                }
                else
                {
                    var newDrug = Drug.Create(medicine.Id, medicine.Description, presentation);
                    await _drugRepository.AddAsync(newDrug);
                }
            }

            var drugsToDisable = dbDrugs
                .Where(x =>
                    !string.IsNullOrWhiteSpace(x.ExternalId) &&
                    !externalIds.Contains(x.ExternalId) &&
                    x.Active).ToList();

            foreach (var drug in drugsToDisable)
            {
                drug.Disable();
                _drugRepository.Update(drug);
            }

            await _drugRepository.SaveChangesAsync();
        }
    }
}