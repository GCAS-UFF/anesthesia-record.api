using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class AuthRepository : RepositoryBase<User>, IAuthRepository
    {
        private readonly SigaDbCtx _context;
        private readonly HttpClient _httpClient;

        public AuthRepository(SigaDbCtx context, IHttpClientFactory factory)
            : base(context)
        {
            _httpClient = factory.CreateClient("HospitalApi");
            _context = context;
        }


        public async Task<UserDto?> LoginAGHU(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
                return null;

            //var response = await _httpClient.GetAsync($"/usuarios?login={login}");

            var response = await _httpClient.GetAsync($"/usuarios");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserDto>();
        }
    }
}