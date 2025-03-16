using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services.EmailServices;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.BackgroundJobs.SupportingPrograms
{
    public class NotifyRegisteredUserJob(
        ILogger<NotifyRegisteredUserJob> logger,
        IEmailService emailService
    ) : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                logger.LogInformation("NotifyRegisteredUserJob started at: {DateTime}", DateTime.UtcNow);

                // Get User and Supporting Program Information
                var dataMap = context.JobDetail.JobDataMap;
                var userId = dataMap.GetString("UserId");
                var userEmail = dataMap.GetString("UserEmail");
                var StartDateAt = dataMap.GetDateTime("StartDateAt");

                // Send Email to notify the starting date of supporting program
                await emailService.SendEmailAsync(
                    userEmail,
                    "Supporting Program Reminder",
                    $"This is a reminder for your supporting program starting at {StartDateAt:g}"
                );

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing NotifyRegisteredUserJob");
                throw new JobExecutionException(ex);
            }
        }
    }
}
