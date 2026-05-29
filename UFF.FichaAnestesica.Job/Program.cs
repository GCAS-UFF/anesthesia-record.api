using Hangfire;
using Hangfire.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("postgresConnection");

builder.Services.AddHangfire(config => config.UsePostgreSqlStorage(connectionString));


builder.Services.AddHangfireServer();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHangfireDashboard("/hangfire");

app.MapControllers();

app.Run();