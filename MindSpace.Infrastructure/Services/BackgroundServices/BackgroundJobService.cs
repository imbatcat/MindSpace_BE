using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services;
using Quartz;

namespace MindSpace.Infrastructure.Services.BackgroundServices
{
    public class BackgroundJobService(
        ISchedulerFactory _schedulerFactory,
        ILogger<BackgroundJobService> _logger
    ) : IBackgroundJobService
    {

        public async Task ScheduleJobWithFireOnce<T>(string sessionId, int minutesFromNow) where T : IJob
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            var job = JobBuilder.Create<T>()
                .WithIdentity(sessionId)
                .UsingJobData("SessionId", sessionId)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{sessionId}.trigger")
                .StartAt(DateBuilder.FutureDate(minutesFromNow, IntervalUnit.Minute))
                .ForJob(sessionId)
                .Build();

            await scheduler.ScheduleJob(job, trigger);
            _logger.LogInformation($"Job {sessionId} scheduled successfully");
        }
    }
}