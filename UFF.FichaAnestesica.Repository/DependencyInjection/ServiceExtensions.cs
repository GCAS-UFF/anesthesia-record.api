using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UFF.FichaAnestesica.Application.Interfaces;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.Aghu;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Services;
using UFF.FichaAnestesica.Infra.Repositories;
using UFF.FichaAnestesica.Infra.Repositories.Aghu;
using UFF.FichaAnestesica.Infra.Services;
using UFF.FichaAnestesica.Service.Services;
using UFF.FichaAnestesica.Service.Services.Aghu;

namespace UFF.FichaAnestesica.Infra.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAnesthesiaRecordRepository, AnesthesiaRecordRepository>();
            services.AddScoped<IProfessionalService, ProfessionalServices>();
            services.AddScoped<IProfessionalReadOnlyRepository, ProfessionalReadOnlyRepository>();
            services.AddScoped<IAnesthesiaRecordService, AnesthesiaRecordService>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<IRazorViewRenderer, RazorViewRendererService>();
            services.AddScoped<IMonitoringRecordService, MonitoringRecordService>();
            services.AddScoped<IMonitoringRecordRepository, MonitoringRecordRepository>();
            services.AddScoped<IPatientReadOnlyRepository, PatientReadOnlyRepository>();
            services.AddScoped<IAuthRepository, AuthReadOnlyRepository>();
            services.AddScoped<IProcedureRepository, ProcedureRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProcedureService, ProcedureService>();
            services.AddScoped<IDrugService, DrugService>();

            services.AddScoped<IProfessionalApiService, ProfessionalApiService>(); 
            services.AddScoped<IMedicineApiService, MedicineApiService>();
            services.AddScoped<IProcedureApiService, ProcedureApiService>();

            services.AddScoped<IMedicineReadOnlyRepository, MedicineReadOnlyRepository>();
            services.AddScoped<IHealthReadOnlyRepository, HealthReadOnlyRepository>();
            services.AddScoped<IProcedureReadOnlyRepository, ProcedureReadOnlyRepository>();
            services.AddScoped<IAghuHttpClientFactory, AghuHttpClientFactory>();

            services.AddScoped<IClinicalEventRepository, ClinicalEventRepository>();
            services.AddScoped<ICustomFieldRepository, CustomFieldRepository>();
            services.AddScoped<ICustomFieldRepository, CustomFieldRepository>();
            services.AddScoped<IDrugRepository, DrugRepository>();
            services.AddScoped<IEventTypeRepository, EventTypeRepository>();
            services.AddScoped<IEventTypeService, EventTypeService>();
            services.AddScoped<IFluidBalanceRepository, FluidBalanceRepository>();
            services.AddScoped<IMonitoringRecordRepository, MonitoringRecordRepository>();
            services.AddScoped<IVitalSignRecordRepository, VitalSignRecordRepository>();
            services.AddScoped<IPreAnesthesiaRecordRepository, PreAnesthesiaRecordRepository>();
            services.AddScoped<IPreAnesthesiaRecordService, PreAnesthesiaRecordService>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<ISurgeryService, SurgeryService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IUserSettingsRepository, UserSettingsRepository>();
            services.AddScoped<IInstitutionSettingsRepository, InstitutionSettingsRepository>();
            services.AddScoped<IUserSettingsService, UserSettingsService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();
            services.AddHttpClient("HospitalApi", client =>
            {
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            return services;
        }
    }
}