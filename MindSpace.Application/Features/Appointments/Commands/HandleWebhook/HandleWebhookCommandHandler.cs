using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.BackgroundJobs;
using MindSpace.Application.Commons.Constants;
using MindSpace.Application.DTOs.Notifications;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Exceptions;
using Stripe;
using Stripe.Checkout;

namespace MindSpace.Application.Features.Appointments.Commands.HandleWebhook;

internal class HandleWebhookCommandHandler(
    IUnitOfWork _unitOfWork,
    INotificationService _notificationService,
    ILogger<HandleWebhookCommandHandler> _logger,
    IBackgroundJobService _backgroundJobService,
    IMapper _mapper) : IRequestHandler<HandleWebhookCommand>
{
    public async Task Handle(HandleWebhookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var stripeEvent = EventUtility.ParseEvent(request.StripeEventJson);

            switch (stripeEvent.Type)
            {
                case EventTypes.CheckoutSessionCompleted:
                    var session = stripeEvent.Data.Object as Session;
                    await HandleCompletedSessionAsync(session!.Id);
                    _logger.LogInformation("Checkout session completed: {0}", session);
                    break;
                case EventTypes.CheckoutSessionExpired:
                    var expiredSession = stripeEvent.Data.Object as Session;
                    await HandleExpiredSessionAsync(expiredSession!.Id);
                    _logger.LogInformation("Checkout session expired: {0}", expiredSession);
                    break;
                default:
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling webhook event: {0}", ex.Message);
            throw;
        }

        async Task HandleExpiredSessionAsync(string sessionId)
        {
            var specification = new AppointmentSpecification(sessionId);
            var appointment = await _unitOfWork.Repository<Appointment>().GetBySpecAsync(specification)
                ?? throw new NotFoundException(nameof(Appointment), sessionId);

            appointment.Status = AppointmentStatus.Failed;
            appointment.PsychologistSchedule.Status = PsychologistScheduleStatus.Free;

            _unitOfWork.Repository<Appointment>().Update(appointment);
            _unitOfWork.Repository<PsychologistSchedule>().Update(appointment.PsychologistSchedule);

            await _notificationService.NotifyPsychologistScheduleFree(UserRoles.Psychologist, _mapper.Map<PsychologistScheduleNotificationResponseDTO>(appointment.PsychologistSchedule));
            await _notificationService.NotifyPsychologistScheduleFree(UserRoles.Student, _mapper.Map<PsychologistScheduleNotificationResponseDTO>(appointment.PsychologistSchedule));
            await _unitOfWork.CompleteAsync();
        }

        async Task HandleCompletedSessionAsync(string sessionId)
        {
            var specification = new AppointmentSpecification(sessionId);
            var appointment = await _unitOfWork.Repository<Appointment>().GetBySpecAsync(specification)
                ?? throw new NotFoundException(nameof(Appointment), sessionId);

            appointment.Status = AppointmentStatus.Success;
            appointment.PsychologistSchedule.Status = PsychologistScheduleStatus.Booked;

            _unitOfWork.Repository<Appointment>().Update(appointment);
            _unitOfWork.Repository<PsychologistSchedule>().Update(appointment.PsychologistSchedule);

            await _notificationService.NotifyPsychologistScheduleBooked(UserRoles.Psychologist, _mapper.Map<PsychologistScheduleNotificationResponseDTO>(appointment.PsychologistSchedule));
            await _notificationService.NotifyPsychologistScheduleBooked(UserRoles.Student, _mapper.Map<PsychologistScheduleNotificationResponseDTO>(appointment.PsychologistSchedule));
            await _unitOfWork.CompleteAsync();

            await ScheduleCreateMeetingRoom(appointment);
        }

        async Task ScheduleCreateMeetingRoom(Appointment appointment)
        {
            var startDate = appointment.PsychologistSchedule.Date;
            var startTime = appointment.PsychologistSchedule.StartTime;

            // Calculate the start time for the meeting room, the room will be created 10 minutes before the appointment actually starts
            var startDateTime = startDate.ToDateTime(startTime.AddMinutes(-AppCts.WebRTC.RoomCreationActualTimeInMinutes));

            await _backgroundJobService.ScheduleJobWithFireOnce<CreateMeetingRoomJob>(
                appointment.Id.ToString(),
                startDateTime
            );
        }
    }
}