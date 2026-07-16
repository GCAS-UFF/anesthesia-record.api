namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public interface IProcedureApiService
    {
        Task<int> SyncProcedures();
    }
}