using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UFF.FichaAnestesica.Api.Middleware;
using UFF.FichaAnestesica.Infra.Context;
using UFF.FichaAnestesica.Infra.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("postgresConnection");

builder.Services.AddDbContext<ISigaDbCtx, SigaDbCtx>(options =>
{
    options.UseNpgsql(connectionString, x =>
     x.MigrationsHistoryTable("__EFMigrationsHistory", "siga_db"));
});

builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:8100",
                "http://10.0.2.2:8100",
                "http://localhost:4200",
                "capacitor://localhost",
                "https://anesthesia-record-app-ionic.web.app",
                "https://anesthesia-record-app-ionic.firebaseapp.com"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UFF - Ficha Anestésica API",
        Version = "v1",
        Description = "API para registro e gerenciamento de anestesia"
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();