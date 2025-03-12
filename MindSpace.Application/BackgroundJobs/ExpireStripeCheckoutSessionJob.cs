using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Interfaces.Services.PaymentServices;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using Quartz;

namespace MindSpace.Application.BackgroundJobs
{
    public class ExpireStripeCheckoutSessionJob(
        ILogger<ExpireStripeCheckoutSessionJob> _logger,
        IUnitOfWork _unitOfWork,
        IStripePaymentService _stripePaymentService,
        INotificationService _notificationService
    ) : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var sessionId = context.JobDetail.JobDataMap.GetString("SessionId");
                _logger.LogInformation("Expiring Stripe checkout session: {SessionId}", sessionId);
                await _stripePaymentService.ExpireStripeCheckoutSession(sessionId!);
                var appointment = await GetAppointment(sessionId!);
                if (appointment == null)
                {
                    _logger.LogError("Appointment not found: {SessionId}", sessionId);
                    return;
                }

                await UpdateAppointmentAndScheduleStatus(appointment);

                //TODO: add signalr here
                _logger.LogInformation("Stripe checkout session expired: {SessionId}", sessionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing Stripe checkout session expiration job");
                throw new JobExecutionException(ex);
            }

            async Task<Appointment> GetAppointment(string sessionId)
            {
                var specification = new AppointmentSpecification(sessionId: sessionId!);
                var appointment = await _unitOfWork.Repository<Appointment>().GetBySpecAsync(specification);
                return appointment;
            }

            async Task UpdateAppointmentAndScheduleStatus(Appointment appointment)
            {
                appointment.Status = AppointmentStatus.Failed;
                var appointmentSchedule = appointment.PsychologistSchedule;
                appointmentSchedule.Status = PsychologistScheduleStatus.Free;
                _unitOfWork.Repository<Appointment>().Update(appointment);
                _unitOfWork.Repository<PsychologistSchedule>().Update(appointmentSchedule);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}