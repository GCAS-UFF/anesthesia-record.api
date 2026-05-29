namespace UFF.FichaAnestesica.Domain.Repositories.ReadOnly
{
    public interface IHealthReadOnlyRepository
    {
        Task<(bool bd, bool aghu)> CheckHealth();
    }
}