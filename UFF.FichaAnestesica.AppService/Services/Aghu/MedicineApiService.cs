using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class MedicineApiService : IMedicineApiService
    {
        private readonly IMedicineReadOnlyRepository _medicineApiReadOnlyRepository;
        private readonly IDrugRepository _drugRepository;

        public MedicineApiService(IMedicineReadOnlyRepository medicineApiReadOnlyRepository, IDrugRepository drugRepository)
        {
            _medicineApiReadOnlyRepository = medicineApiReadOnlyRepository;
            _drugRepository = drugRepository;
        }

        public async Task<int> SyncMedicines()
        {
            var response = await _medicineApiReadOnlyRepository.GetDrugsFromAGHU();

            if (response != null && response.Drugs == null || !response.Drugs.Any())
                return 0;

            var dbDrugs = await _drugRepository.GetAllAsync();
            var drugsByExternalId = dbDrugs.Where(x => !string.IsNullOrWhiteSpace(x.ExternalId)).ToDictionary(x => x.ExternalId);
            var externalIds = new HashSet<string>();

            foreach (var medicine in response.Drugs)
            {
                if (string.IsNullOrWhiteSpace(medicine.Codigo))
                    continue;

                externalIds.Add(medicine.Codigo);

                if (drugsByExternalId.TryGetValue(medicine.Codigo, out var existingDrug))
                {
                    existingDrug.Update(medicine.Description, medicine.Unity);
                    _drugRepository.Update(existingDrug);
                }
                else
                {
                    var newDrug = Drug.Create(medicine.Codigo, medicine.Description, medicine.Unity);
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

            return response.Drugs.Count();
        }
    }
}