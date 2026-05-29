using Microsoft.EntityFrameworkCore;
using Quartz;
using UFF.FichaAnestesica.Infra.Context;
using UFF.FichaAnestesica.Job;
using UFF.FichaAnestesica.Job.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<SigaDbCtx>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("postgresConnection")));
builder.Services.RegisterJobServices2(builder.Configuration);

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("SyncAghuJob");

    q.AddJob<AghuSyncJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("SyncAghuTrigger")
        .StartNow()
        .WithSimpleSchedule(x => x.WithIntervalInMinutes(30).RepeatForever()));
});

builder.Services.AddQuartzHostedService(q =>
{
    q.WaitForJobsToComplete = true;
});

var app = builder.Build();

app.MapControllers();
app.Run();