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
        public async Task ScheduleJobWithFireOnce<T>(
            string referenceId,
            int minutesFromNow,
            Dictionary<string, object> jobDatas = null
            ) where T : IJob
        {
            // Create Scheduler
            var scheduler = await _schedulerFactory.GetScheduler();

            // Configure a jobBuilder
            var jobBuilder = JobBuilder.Create<T>()
                .WithIdentity(referenceId)
                .UsingJobData(nameof(referenceId), referenceId);

            // Add extra jobBuilder data
            if (jobDatas != null)
            {
                foreach (KeyValuePair<string, object> data in jobDatas)
                {
                    jobBuilder = jobBuilder.UsingJobData(data.Key, data.Value.ToString());
                }
            }

            // Build a Job
            var job = jobBuilder.Build();

            // Schedule a jobBuilder with simple trigger
            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{referenceId}.trigger")
                .StartAt(DateBuilder.FutureDate(minutesFromNow, IntervalUnit.Minute))
                .ForJob(referenceId)
                .Build();

            await scheduler.ScheduleJob(job, trigger);
            _logger.LogInformation($"Job {referenceId} scheduled successfully");
        }

        public async Task ScheduleJobWithFireOnce<T>(
            string referenceId,
            DateTime dateTime,
            Dictionary<string, object> jobDatas = null) where T : IJob
        {
            // Create scheduler
            var scheduler = await _schedulerFactory.GetScheduler();

            // Configure a jobBuilder
            var jobBuilder = JobBuilder.Create<T>()
                .WithIdentity(referenceId)
                .UsingJobData(nameof(referenceId), referenceId);

            // Add extra jobBuilder data
            if (jobDatas != null)
            {
                foreach (KeyValuePair<string, object> data in jobDatas)
                {
                    jobBuilder = jobBuilder.UsingJobData(data.Key, data.Value.ToString());
                }
            }

            // Build a Job
            var job = jobBuilder.Build();

            // Schedule a jobBuilder with simple trigger
            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{referenceId}.trigger")
                .StartAt(dateTime)
                .ForJob(referenceId)
                .Build();

            await scheduler.ScheduleJob(job, trigger);
            _logger.LogInformation($"Job {referenceId} scheduled successfully");
        }

    }
}