using Microsoft.Extensions.DependencyInjection;

namespace UFF.FichaAnestesica.Infra.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            //services.AddScoped<IPatientService, PatientService>();

            return services;
        }
    }
}
