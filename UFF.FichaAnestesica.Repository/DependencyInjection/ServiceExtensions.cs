using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UFF.FichaAnestesica.Application.Interfaces;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Services;
using UFF.FichaAnestesica.Infra.Repositories;
using UFF.FichaAnestesica.Infra.Repositories.ReadOnly;
using UFF.FichaAnestesica.Infra.Services;
using UFF.FichaAnestesica.Service.Services;

namespace UFF.FichaAnestesica.Infra.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILdapAuthReadOnlyRepository, LdapAuthReadOnlyRepository>();  
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAnesthesiaRecordRepository, AnesthesiaRecordRepository>();
            services.AddScoped<IProfessionalService, ProfessionalServices>();
            services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
            services.AddScoped<IHospitalApiRepository, HospitalApiRepository>();
            services.AddScoped<IAnesthesiaRecordService, AnesthesiaRecordService>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<IRazorViewRenderer, RazorViewRenderer>();

            services.AddHttpContextAccessor();
            services.AddScoped<IPdfService, PdfService>();

            services.AddScoped<ISurgeryService, SurgeryService>();
            services.AddScoped<IAuthService, AuthService>();

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