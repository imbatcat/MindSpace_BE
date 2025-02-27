using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services.PaymentServices;
using MindSpace.Application.Interfaces.Services.SignalR;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Application.Specifications.PsychologistScheduleSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Appointments.Commands.ConfirmBookingAppointment;

public class ConfirmBookingAppointmentCommandHandler : IRequestHandler<ConfirmBookingAppointmentCommand, ConfirmBookingAppointmentResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISignalRNotification _signalRNotification;
    private readonly ILogger<ConfirmBookingAppointmentCommandHandler> _logger;
    private readonly IStripePaymentService _stripePaymentService;
    private readonly IConfiguration _configuration;
    public ConfirmBookingAppointmentCommandHandler(
        IUnitOfWork unitOfWork,
        ISignalRNotification signalRNotification,
        ILogger<ConfirmBookingAppointmentCommandHandler> logger,
        IStripePaymentService stripePaymentService,
        IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _signalRNotification = signalRNotification;
        _logger = logger;
        _stripePaymentService = stripePaymentService;
        _configuration = configuration;
    }

    public async Task<ConfirmBookingAppointmentResult> Handle(ConfirmBookingAppointmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Confirming booking appointment for psychologist {PsychologistId}", request.PsychologistId);

            var appointment = await TryGetAppointmentAsync();
            if (appointment != null)
            {
                return new ConfirmBookingAppointmentResult
                {
                    SessionId = appointment.SessionId,
                    SessionUrl = "placeholder, this does nothing"
                };
            }
            var scheduleWithPsychologist = await GetScheduleWithPsychologistAsync();
            var psychologist = scheduleWithPsychologist.Psychologist;

            // Validate schedule belongs to psychologist
            if (scheduleWithPsychologist.PsychologistId != request.PsychologistId)
            {
                throw new UnauthorizedAccessException($"Schedule {request.ScheduleId} does not belong to psychologist {request.PsychologistId}");
            }

            var (sessionId, sessionUrl) = _stripePaymentService.CreateCheckoutSession(psychologist.SessionPrice, psychologist.ComissionRate);

            _logger.LogInformation("Successfully created checkout session");

            await UpdateAppointemntAndScheduleAsync(sessionId, scheduleWithPsychologist);

            await _signalRNotification.NotifyScheduleStatus(scheduleWithPsychologist);

            return new ConfirmBookingAppointmentResult
            {
                SessionId = sessionId,
                SessionUrl = sessionUrl
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error confirming booking appointment");
            throw;
        }

        async Task<PsychologistSchedule> GetScheduleWithPsychologistAsync()
        {
            var scheduleSpecification = new PsychologistScheduleSpecification(request.ScheduleId, includePsychologist: true);
            var scheduleWithPsychologist = await _unitOfWork.Repository<PsychologistSchedule>().GetBySpecAsync(scheduleSpecification)
                ?? throw new NotFoundException(nameof(PsychologistSchedule), request.ScheduleId.ToString());
            return scheduleWithPsychologist;
        }

        async Task<Appointment?> TryGetAppointmentAsync()
        {
            var sessionExpirationMinutes = _configuration.GetValue<int>("Stripe:SessionExpirationMinutes");
            var appointmentSpecification = new AppointmentSpecification(request.StudentId, request.PsychologistId, request.ScheduleId, sessionExpirationMinutes);
            var appointment = await _unitOfWork.Repository<Appointment>().GetBySpecAsync(appointmentSpecification);
            return appointment;
        }

        async Task UpdateAppointemntAndScheduleAsync(string sessionId, PsychologistSchedule psychologistSchedule)
        {
            var newAppointment = new Appointment
            {
                CreateAt = DateTime.UtcNow.ToLocalTime(),
                UpdateAt = DateTime.UtcNow.ToLocalTime(),
                Status = AppointmentStatus.Pending,
                StudentId = request.StudentId,
                PsychologistId = request.PsychologistId,
                PsychologistScheduleId = request.ScheduleId,
                SpecializationId = request.SpecializationId,
                MeetURL = String.Empty,
                SessionId = sessionId
            };
            psychologistSchedule.Status = PsychologistScheduleStatus.Locked;
            _unitOfWork.Repository<Appointment>().Insert(newAppointment);
            _logger.LogInformation("Successfully inserted new appointment");
            _unitOfWork.Repository<PsychologistSchedule>().Update(psychologistSchedule);
            await _unitOfWork.CompleteAsync();
        }
    }
}