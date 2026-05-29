using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Infra.Context;
using UFF.FichaAnestesica.Infra.Repositories;
using UFF.FichaAnestesica.Infra.Repositories.Aghu;
using UFF.FichaAnestesica.Service.Services.Aghu;

namespace UFF.FichaAnestesica.Job
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterJobServices2(this IServiceCollection services, IConfiguration configuration)
        {         
            services.AddDbContext<SigaDbCtx>(options => options.UseNpgsql(configuration.GetConnectionString("postgresConnection")));

            services.AddScoped<IProfessionalApiService, ProfessionalApiService>();
            services.AddScoped<IMedicineApiService, MedicineApiService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDrugRepository, DrugRepository>();

            services.AddScoped<IProfessionalReadOnlyRepository, ProfessionalReadOnlyRepository>();
            services.AddScoped<IMedicineReadOnlyRepository, MedicineReadOnlyRepository>();
            services.AddScoped<IHealthReadOnlyRepository, HealthReadOnlyRepository>();

            var hospitalApiUrl = configuration["HospitalApi:BaseUrl"];

            services.AddHttpClient("HospitalApi", client =>
            {
                client.BaseAddress = new Uri(hospitalApiUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            return services;
        }
    }
}