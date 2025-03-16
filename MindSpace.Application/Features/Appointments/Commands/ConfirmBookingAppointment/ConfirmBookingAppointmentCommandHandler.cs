using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.BackgroundJobs.Payments;
using MindSpace.Application.Commons.Constants;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.DTOs.Notifications;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Interfaces.Services.PaymentServices;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Application.Specifications.PsychologistScheduleSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Exceptions;
using static MindSpace.Application.Commons.Constants.AppCts.StripePayment;

namespace MindSpace.Application.Features.Appointments.Commands.ConfirmBookingAppointment;

public class ConfirmBookingAppointmentCommandHandler(
    IUnitOfWork unitOfWork,
    ILogger<ConfirmBookingAppointmentCommandHandler> logger,
    INotificationService notificationService,
    IStripePaymentService stripePaymentService,
    IBackgroundJobService backgroundJobService,
    IMapper mapper) : IRequestHandler<ConfirmBookingAppointmentCommand, ConfirmBookingAppointmentResultDTO>
{
    public async Task<ConfirmBookingAppointmentResultDTO> Handle(ConfirmBookingAppointmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Confirming booking appointment for psychologist {PsychologistId}", request.PsychologistId);

            var appointment = await TryGetAppointmentAsync();
            if (appointment != null)
            {
                return new ConfirmBookingAppointmentResultDTO
                {
                    SessionId = appointment.SessionId,
                };
            }
            var scheduleWithPsychologist = await GetScheduleWithPsychologistAsync();
            var psychologist = scheduleWithPsychologist.Psychologist;

            // Validate schedule belongs to psychologist
            if (scheduleWithPsychologist.PsychologistId != request.PsychologistId)
            {
                throw new UnauthorizedAccessException($"Schedule {request.ScheduleId} does not belong to psychologist {request.PsychologistId}");
            }

            var sessionId = stripePaymentService.CreateCheckoutSession(psychologist.SessionPrice, psychologist.ComissionRate);

            await ScheduleSessionExpirationJob(sessionId);

            logger.LogInformation("Successfully created checkout session for student {StudentId}", request.StudentId);

            await UpdateAppointmentAndScheduleAsync(sessionId, scheduleWithPsychologist);

            logger.LogInformation("Successfully inserted new appointment with sessionId {SessionId}, and updated schedule", sessionId);

            // Notify student and psychologist to lock the schedule
            await notificationService.NotifyPsychologistScheduleLocked(UserRoles.Student, mapper.Map<PsychologistScheduleNotificationResponseDTO>(scheduleWithPsychologist));

            await notificationService.NotifyPsychologistScheduleLocked(UserRoles.Psychologist, mapper.Map<PsychologistScheduleNotificationResponseDTO>(scheduleWithPsychologist));

            return new ConfirmBookingAppointmentResultDTO
            {
                SessionId = sessionId,
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error confirming booking appointment");
            throw;
        }

        async Task<PsychologistSchedule> GetScheduleWithPsychologistAsync()
        {
            var scheduleSpecification = new PsychologistScheduleSpecification(request.ScheduleId, includePsychologist: true);
            var scheduleWithPsychologist = await unitOfWork.Repository<PsychologistSchedule>().GetBySpecAsync(scheduleSpecification)
                ?? throw new NotFoundException(nameof(PsychologistSchedule), request.ScheduleId.ToString());
            return scheduleWithPsychologist;
        }

        async Task<Appointment?> TryGetAppointmentAsync()
        {
            var appointmentSpecification = new AppointmentSpecification(request.StudentId, request.PsychologistId, request.ScheduleId, CheckoutSessionExpireTimeInMinutes);
            var appointment = await unitOfWork.Repository<Appointment>().GetBySpecAsync(appointmentSpecification);
            return appointment;
        }

        async Task UpdateAppointmentAndScheduleAsync(string sessionId, PsychologistSchedule psychologistSchedule)
        {
            var newAppointment = new Appointment
            {
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                Status = AppointmentStatus.Pending,
                StudentId = request.StudentId,
                PsychologistId = request.PsychologistId,
                PsychologistScheduleId = request.ScheduleId,
                SpecializationId = request.SpecializationId,
                MeetURL = String.Empty,
                SessionId = sessionId
            };
            psychologistSchedule.Status = PsychologistScheduleStatus.Locked;
            unitOfWork.Repository<Appointment>().Insert(newAppointment);
            unitOfWork.Repository<PsychologistSchedule>().Update(psychologistSchedule);
            await unitOfWork.CompleteAsync();
        }

        async Task ScheduleSessionExpirationJob(string sessionId)
        {
            var jobKey = $"{nameof(ExpireStripeCheckoutSessionJob)}-{sessionId}";
            await backgroundJobService.ScheduleJobWithFireOnce<ExpireStripeCheckoutSessionJob>(jobKey, CheckoutSessionExpireTimeInMinutes);
        }
    }
}