using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class ProfessionalReadOnlyRepository : RepositoryBase<User>, IProfessionalReadOnlyRepository
    {
        private readonly HttpClient _httpClient;
        private readonly SigaDbCtx _context;

        public ProfessionalReadOnlyRepository(
            SigaDbCtx context,
            IHttpClientFactory factory)
            : base(context)
        {
            _context = context;
            _httpClient = factory.CreateClient("HospitalApi");
        }

        public async Task<List<User>> GetProfessionalsForAnethesiaRecord(string name)
        {
            return await _context.Users
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<UserListDto> GetProfessionalsFromAGHU()
        {
            var response = await _httpClient.GetAsync("/profissionais");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserListDto>() ?? new UserListDto();
        }
    }
}