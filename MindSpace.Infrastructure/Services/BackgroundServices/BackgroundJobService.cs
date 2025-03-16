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
        public async Task ScheduleJobWithFireOnce<T>(string referenceId, int minutesFromNow) where T : IJob
        {
            // Create Scheduler
            var scheduler = await _schedulerFactory.GetScheduler();

            // Configure a job
            var job = JobBuilder.Create<T>()
                .WithIdentity(referenceId)
                .UsingJobData(nameof(referenceId), referenceId)
                .Build();

            // Schedule a job with trigger
            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{referenceId}.trigger")
                .StartAt(DateBuilder.FutureDate(minutesFromNow, IntervalUnit.Minute))
                .ForJob(referenceId)
                .Build();

            await scheduler.ScheduleJob(job, trigger);
            _logger.LogInformation($"Job {referenceId} scheduled successfully");
        }

        public Task ScheuleJobWithFireOnDate<T>(string referenceId, TimeSpan timeSpan) where T : IJob
        {
            throw new NotImplementedException();
        }
    }
}