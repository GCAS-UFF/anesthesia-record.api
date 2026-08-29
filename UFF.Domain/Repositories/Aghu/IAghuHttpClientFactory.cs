namespace UFF.FichaAnestesica.Domain.Repositories.Aghu
{
    public interface IAghuHttpClientFactory
    {
        Task<HttpClient> CreateClientAsync();
    }
}
