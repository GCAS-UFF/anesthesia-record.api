using Microsoft.Extensions.DependencyInjection;
using UFF.FichaAnestesica.Domain.Services;
using UFF.FichaAnestesica.Service.Services;


namespace UFF.FichaAnestesica.Infra.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ISurgeryService, SurgeryService>();

            return services;
        }
    }
}
