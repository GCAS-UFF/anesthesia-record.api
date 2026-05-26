using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly SigaDbCtx _context;
        private readonly HttpClient _httpClient;

        public UserRepository(SigaDbCtx context, IHttpClientFactory factory)
            : base(context)
        {
            _httpClient = factory.CreateClient("HospitalApi");
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
            => await _context.Users.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<User> GetUserByLoginAsync(string login)
            => await _context.Users.FirstOrDefaultAsync(p => p.Login == login);

        public async Task<UserDto?> GetUserFromApiByLoginAsync(string login)
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