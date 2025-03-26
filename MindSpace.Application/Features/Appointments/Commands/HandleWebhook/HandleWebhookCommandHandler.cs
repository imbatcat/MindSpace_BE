using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.BackgroundJobs.Appointments;
using MindSpace.Application.BackgroundJobs.MeetingRooms;
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
using Invoice = MindSpace.Domain.Entities.Appointments.Invoice;
using PaymentMethod = MindSpace.Domain.Entities.Constants.PaymentMethod;

namespace MindSpace.Application.Features.Appointments.Commands.HandleWebhook;

internal class HandleWebhookCommandHandler(
        IUnitOfWork _unitOfWork,
        INotificationService _notificationService,
        ILogger<HandleWebhookCommandHandler> _logger,
        IBackgroundJobService _backgroundJobService,
        IMapper _mapper,
        IPaymentNotificationService _paymentNotificationService,
        IConfiguration _configuration
    ) : IRequestHandler<HandleWebhookCommand>
{
    public async Task Handle(HandleWebhookCommand request, CancellationToken cancellationToken)
    {
        var webhookSecret = _configuration["Stripe:WebhookSecret"];
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(request.StripeEventJson, request.StripeSignature, webhookSecret);

            switch (stripeEvent.Type)
            {
                case EventTypes.CheckoutSessionCompleted:
                    var session = stripeEvent.Data.Object as Session;
                    await HandleCompletedSessionAsync(session);
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
            // Flow: 
            // 1. Create an appointment specification using the session ID.
            // 2. Retrieve the appointment from the repository based on the specification.
            // 3. If the appointment is not found, throw a NotFoundException.
            // 4. Notify the psychologist and student that the schedule is free.
            // 5. Notify the appointment status to 'Failed'.
            // 6. Update the psychologist schedule status to 'Free'.
            // 7. Update the appointment and psychologist schedule in the repository.
            // 8. Complete the unit of work to save changes.

            var specification = new AppointmentSpecification(sessionId, AppointmentSpecification.StringParameterType.SessionId);
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

        async Task HandleCompletedSessionAsync(Session session)
        {
            // Handle the completed session by updating the appointment status, notifying relevant parties, creating an invoice, scheduling a meeting room, and notifying payment success.
            // Flow: 
            // 1. Retrieve session ID from the completed session.
            // 2. Get the appointment using the session ID.
            // 3. Update appointment and psychologist schedule statuses.
            // 4. Notify psychologist and student about the booking.
            // 5. Create an invoice for the appointment.
            // 6. Schedule a meeting room for the appointment.
            // 7. Notify payment success to the frontend.

            var sessionId = session.Id;

            var specification = new AppointmentSpecification(sessionId, AppointmentSpecification.StringParameterType.SessionId);
            var appointment = await _unitOfWork.Repository<Appointment>().GetBySpecAsync(specification)
                ?? throw new NotFoundException(nameof(Appointment), sessionId);

            appointment.Status = AppointmentStatus.Success;
            appointment.PsychologistSchedule.Status = PsychologistScheduleStatus.Booked;

            _unitOfWork.Repository<Appointment>().Update(appointment);
            _unitOfWork.Repository<PsychologistSchedule>().Update(appointment.PsychologistSchedule);

            await _notificationService.NotifyPsychologistScheduleBooked(UserRoles.Psychologist, _mapper.Map<PsychologistScheduleNotificationResponseDTO>(appointment.PsychologistSchedule));
            await _notificationService.NotifyPsychologistScheduleBooked(UserRoles.Student, _mapper.Map<PsychologistScheduleNotificationResponseDTO>(appointment.PsychologistSchedule));
            await _unitOfWork.CompleteAsync();

            await CreateInvoice(appointment, session);

            await ScheduleCreateMeetingRoom(appointment);

            await _paymentNotificationService.NotifyPaymentSuccess(session.Id);
        }

        async Task ScheduleCreateMeetingRoom(Appointment appointment)
        {
            var startDate = appointment.PsychologistSchedule.Date;
            var startTime = appointment.PsychologistSchedule.StartTime;

            var startDateTime = startDate.ToDateTime(startTime);

            Dictionary<string, object> createMeetingRoomJobData = new() {
                { "AppointmentTime", startDateTime }
            };
            await _backgroundJobService.ScheduleJobWithFireOnce<CreateMeetingRoomJob>(
                $"create_room-{appointment.SessionId}",
                startDateTime,
                createMeetingRoomJobData
            );

            Dictionary<string, object> reminderAppointmentJobData = new() {
                { "AppointmentId", appointment.Id },
                { "StudentEmail", appointment.Student.Email! },
                { "PsychologistEmail", appointment.Psychologist.Email! },
                { "AppointmentTime", startDateTime }
            };
            await _backgroundJobService.ScheduleJobWithFireOnce<AppointmentReminderJob>(
                $"reminder-appointment-{appointment.SessionId}",
                0,
                reminderAppointmentJobData
            );
        }

        async Task CreateInvoice(Appointment appointment, Session session)
        {
            _logger.LogInformation("Creating invoice for appointment: {0}", appointment.Id);
            Invoice invoice = new()
            {
                AppointmentId = appointment.Id,
                TransactionCode = session.Id,
                PaymentType = PaymentType.Purchase,
                Provider = AppCts.StripePayment.Provider,
                PaymentMethod = PaymentMethod.STRIPE,
                PaymentDescription = "Payment for appointment",
                Amount = appointment.Psychologist.SessionPrice,
                AccountId = appointment.StudentId,
                TransactionTime = DateTime.Now
            };
            _unitOfWork.Repository<Invoice>().Insert(invoice);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation("Invoice created for appointment: {0}", appointment.Id);
        }
    }
}