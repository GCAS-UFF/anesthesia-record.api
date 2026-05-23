using Microsoft.Extensions.DependencyInjection;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Services;
using UFF.FichaAnestesica.Infra.Repositories;
using UFF.FichaAnestesica.Infra.Repositories.ReadOnly;
using UFF.FichaAnestesica.Service.Services;

namespace UFF.FichaAnestesica.Infra.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IHospitalReadOnlyRepository, HospitalReadOnlyRepository>();
            services.AddScoped<ILdapAuthReadOnlyRepository, LdapAuthReadOnlyRepository>();
            services.AddScoped<ISurgeryRepository, SurgeryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
           
            services.AddScoped<ISurgeryService, SurgeryService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}