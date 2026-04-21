using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UFF.FichaAnestesica.Api.Middleware;
using UFF.FichaAnestesica.Infra.Context;
using UFF.FichaAnestesica.Infra.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("postgresConnection");
var connectionStringReadOnly = builder.Configuration.GetConnectionString("postgresConnectionReadOnly");

// Registrar DbContexts com suas interfaces
builder.Services.AddDbContext<ISigaDbCtx, SigaDbCtx>(options =>
{
    options.UseNpgsql(connectionString, x =>
     x.MigrationsHistoryTable("__EFMigrationsHistory", "siga_db"));
});

builder.Services.AddDbContext<ISigaDbReadOnlyCtx, SigaDbReadOnlyCtx>(options =>
{
    options.UseNpgsql(connectionStringReadOnly);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// Registrar serviços
builder.Services.RegisterServices();

builder.Services.AddControllers();
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

app.MapGet("/", () => "UFF - API rodando 🚀");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();