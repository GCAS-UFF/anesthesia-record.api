using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UFF.FichaAnestesica.Api.Middleware;
using UFF.FichaAnestesica.Infra.Context;
using UFF.FichaAnestesica.Infra.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbiUffFichaAnestesicaContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("postgresConnection");
    options.UseNpgsql(connectionString);
});

builder.Services.RegisterServices();

builder.Services.AddControllers();
builder.Services.AddHttpClient(); // Adicionado para consumirmos a API mockada do hospital em PHP
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseMiddleware<ExceptionMiddleware>(); // Adiciona para padronizar a centralizar as possíveis exceções que podem acontecer

app.MapGet("/", () => "API rodando 🚀");

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
