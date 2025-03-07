using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.DTOs.Notifications;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Interfaces.Services.PaymentServices;
using MindSpace.Application.Notifications;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Application.Specifications.PsychologistScheduleSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Appointments.Commands.ConfirmBookingAppointment;

public class ConfirmBookingAppointmentCommandHandler : IRequestHandler<ConfirmBookingAppointmentCommand, ConfirmBookingAppointmentResultDTO>
{
    // ==============================
    // === Fields & Props
    // ==============================

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ConfirmBookingAppointmentCommandHandler> _logger;
    private readonly INotificationService _notificationService;
    private readonly IStripePaymentService _stripePaymentService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    // ==============================
    // === Constructors
    // ==============================

    public ConfirmBookingAppointmentCommandHandler(IUnitOfWork unitOfWork, ILogger<ConfirmBookingAppointmentCommandHandler> logger, INotificationService notificationService, IStripePaymentService stripePaymentService, IConfiguration configuration, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _notificationService = notificationService;
        _stripePaymentService = stripePaymentService;
        _configuration = configuration;
        _mapper = mapper;
    }

    // ==============================
    // === Methods
    // ==============================

    public async Task<ConfirmBookingAppointmentResultDTO> Handle(ConfirmBookingAppointmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Confirming booking appointment for psychologist {PsychologistId}", request.PsychologistId);

            var appointment = await TryGetAppointmentAsync();
            if (appointment != null)
            {
                return new ConfirmBookingAppointmentResultDTO
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

            // Notify student and psychologist to lock the schedule
            await _notificationService.NotifyPsychologistScheduleLocked(UserRoles.Student, _mapper.Map<PsychologistScheduleNotificationResponseDTO>(scheduleWithPsychologist));
            await _notificationService.NotifyPsychologistScheduleLocked(UserRoles.Psychologist, _mapper.Map<PsychologistScheduleNotificationResponseDTO>(scheduleWithPsychologist));

            return new ConfirmBookingAppointmentResultDTO
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