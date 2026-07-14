namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public interface IMedicineApiService
    {
        Task<int> SyncMedicines();
    }
}