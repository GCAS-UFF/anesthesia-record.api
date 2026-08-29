using System.Net;
using System.Net.Http.Json;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.Aghu;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class AuthReadOnlyRepository : RepositoryBase<User>, IAuthRepository
    {
        private readonly SigaDbCtx _context;
        private readonly IAghuHttpClientFactory _aghuHttpClientFactory;

        public AuthReadOnlyRepository(SigaDbCtx context, IAghuHttpClientFactory aghuHttpClientFactory)
            : base(context)
        {
            _aghuHttpClientFactory = aghuHttpClientFactory;
            _context = context;
        }

        public async Task<UserDto?> LoginAGHU(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
                return null;

            var client = await _aghuHttpClientFactory.CreateClientAsync();
            var response = await client.PostAsJsonAsync("auth", new
            {
                Login = login,
                Senha = password
            });

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            var user = await response.Content.ReadFromJsonAsync<UserDto>();

            return user;
        }
    }
}