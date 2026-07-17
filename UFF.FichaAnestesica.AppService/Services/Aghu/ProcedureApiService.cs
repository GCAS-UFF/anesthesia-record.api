using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class ProcedureApiService : IProcedureApiService
    {
        private readonly IProcedureReadOnlyRepository _procedureReadOnlyRepository;
        private readonly IProcedureRepository _procedureRepository;

        public ProcedureApiService(IProcedureReadOnlyRepository procedureReadOnlyRepository, IProcedureRepository procedureRepository)
        {
            _procedureReadOnlyRepository = procedureReadOnlyRepository;
            _procedureRepository = procedureRepository;
        }
      
        public async Task<int> SyncProcedures()
        {
            var response = await _procedureReadOnlyRepository.GetProceduresFromAGHU();

            if (response != null && response.Procedures == null || response != null && !response.Procedures.Any())
                return 0;

            var dbProcedures = await _procedureRepository.GetAllAsync();
            var proceduresByExternalId = dbProcedures.Where(x => !string.IsNullOrWhiteSpace(x.ExternalId)).ToDictionary(x => x.ExternalId);
            var externalIds = new HashSet<string>();

            foreach (var procedure in response.Procedures)
            {
                if (string.IsNullOrWhiteSpace(procedure.ExternalId.ToString()))
                    continue;

                externalIds.Add(procedure.ExternalId.ToString());

                if (proceduresByExternalId.TryGetValue(procedure.ExternalId.ToString(), out var existingProcedure))
                {
                    existingProcedure.Update(procedure.Codigo, procedure.Description, procedure.Cid);
                    _procedureRepository.Update(existingProcedure);
                }
                else
                {
                    var newProcedure = Procedure.Create(procedure.ExternalId.ToString(), procedure.Codigo, procedure.Description, procedure.Cid);
                    await _procedureRepository.AddAsync(newProcedure);
                }
            }

            var proceduresToDisable = dbProcedures
                                    .Where(x =>
                                        !string.IsNullOrWhiteSpace(x.ExternalId) &&
                                        !externalIds.Contains(x.ExternalId) &&
                                        x.Active).ToList();


            foreach (var procedure in proceduresToDisable)
            {
                procedure.Disable();
                _procedureRepository.Update(procedure);
            }

            await _procedureRepository.SaveChangesAsync();
            return response.Procedures.Count();
        }
    }
}