using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using QuartzSample.Jobs;
using QuartzSample.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IFileService, FileService>();
        services.AddQuartz(opt =>
        {
            var jobKey = new JobKey("Test Job Key");
            opt.AddJob<TestJob>(jobOpt =>
            {
                jobOpt.WithIdentity(jobKey);
            });

            opt.AddTrigger(triggerOpt =>
            {
                triggerOpt.ForJob(jobKey)
                    .WithIdentity("Trigger Identity")
                        .StartNow()
                            .WithSimpleSchedule(scheduleOpt =>
                            {
                                scheduleOpt.WithIntervalInMinutes(5)
                                    .RepeatForever();
                            });
            });
        });
        services.AddQuartzHostedService(opt =>
        {
            opt.WaitForJobsToComplete = true;
        });
    })
    .Build();

host.Run();
