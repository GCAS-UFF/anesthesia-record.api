using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories.Aghu;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class ProfessionalReadOnlyRepository : RepositoryBase<User>, IProfessionalReadOnlyRepository
    {
        private readonly IAghuHttpClientFactory _aghuHttpClientFactory;
        private readonly SigaDbCtx _context;

        public ProfessionalReadOnlyRepository(SigaDbCtx context, IAghuHttpClientFactory aghuHttpClientFactory) : base(context)
        {
            _context = context;
            _aghuHttpClientFactory = aghuHttpClientFactory;
        }

        public async Task<List<User>> GetProfessionalsForAnethesiaRecord(string term)
        {
            return await _context.Users
                .Where(x => x.Name.ToLower().Contains(term.ToLower()) || x.Registration.ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<DateTime?> GetLastTimeIntegration()
        {
            var professional = await _context.Users.FirstOrDefaultAsync();
            return professional == null ? null : professional.LastSyncAt;
        }

        public async Task<List<User>> GetAllProfessionalsForAnethesiaRecord()
        {
            return await _context.Users
                .Where(x => x.Status == Domain.Enums.UserStatusEnum.Enabled)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<UserListDto> GetProfessionalsFromAGHU()
        {
            var client = await _aghuHttpClientFactory.CreateClientAsync();
            var response = await client.GetAsync("profissionais");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserListDto>() ?? new UserListDto();
        }
    }
}