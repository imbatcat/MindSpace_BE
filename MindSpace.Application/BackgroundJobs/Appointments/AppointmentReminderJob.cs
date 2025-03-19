using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services.EmailServices;
using Quartz;

namespace MindSpace.Application.BackgroundJobs.Appointments
{
    [DisallowConcurrentExecution]
    public class AppointmentReminderJob(
        ILogger<AppointmentReminderJob> _logger,
        IEmailService _emailService
    ) : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation($"Appointment reminder job started at: {DateTime.Now}");

                // Get job data from the context
                var dataMap = context.JobDetail.JobDataMap;
                var appointmentId = dataMap.GetString("AppointmentId");
                var studentEmail = dataMap.GetString("StudentEmail");
                var psychologistEmail = dataMap.GetString("PsychologistEmail");
                var appointmentTime = dataMap.GetDateTime("AppointmentTime");

                // Send reminder email
                await _emailService.SendEmailAsync(
                    studentEmail,
                    "Appointment Reminder",
                    $"This is a reminder for your appointment scheduled at {appointmentTime:g}");

                await _emailService.SendEmailAsync(
                    psychologistEmail,
                    "Appointment Reminder",
                    $"This is a reminder for your appointment scheduled at {appointmentTime:g}");

                _logger.LogInformation($"Appointment reminder sent successfully for appointment {appointmentId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing appointment reminder job");
                throw new JobExecutionException(ex);
            }
        }
    }
}