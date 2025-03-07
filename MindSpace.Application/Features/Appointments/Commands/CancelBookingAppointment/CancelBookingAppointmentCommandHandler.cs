using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Notifications;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Appointments.Commands.CancelBookingAppointment
{
    public class CancelBookingAppointmentCommandHandler : IRequestHandler<CancelBookingAppointmentCommand>
    {
        // ==============================
        // === Fields & Props
        // ==============================

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CancelBookingAppointmentCommandHandler> _logger;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        // ==============================
        // === Constructors
        // ==============================

        public CancelBookingAppointmentCommandHandler(IUnitOfWork unitOfWork, ILogger<CancelBookingAppointmentCommandHandler> logger, INotificationService notificationService, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _notificationService = notificationService;
            _configuration = configuration;
            _mapper = mapper;
        }

        // ==============================
        // === Methods
        // ==============================

        public async Task Handle(CancelBookingAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Cancelling booking appointment for student {StudentId}", request.StudentId);

                var (appointment, schedule) = await UpdateAppointemntAndScheduleAsync(request.StudentId, request.PsychologistId, request.ScheduleId);

                // Notify schedule free to both student and psychologist
                await _notificationService.NotifyPsychologistScheduleFree(
                    UserRoles.Student,
                    _mapper.Map<PsychologistScheduleNotificationResponseDTO>(schedule));

                await _notificationService.NotifyPsychologistScheduleFree(
                    UserRoles.Psychologist,
                    _mapper.Map<PsychologistScheduleNotificationResponseDTO>(schedule));

                _logger.LogInformation("Successfully cancelled booking appointment for student {StudentId}", request.StudentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling booking appointment for student {StudentId}", request.StudentId);
                throw;
            }

            async Task<(Appointment, PsychologistSchedule)> UpdateAppointemntAndScheduleAsync(int studentId, int psychologistId, int scheduleId)
            {

                var sessionExpirationMinutes = _configuration.GetValue<int>("Stripe:SessionExpirationMinutes");
                var appointmentSpecification = new AppointmentSpecification(request.StudentId, request.PsychologistId, request.ScheduleId);
                var appointment = await _unitOfWork.Repository<Appointment>().GetBySpecAsync(appointmentSpecification)
                    ?? throw new NotFoundException(nameof(Appointment), request.StudentId.ToString());


                appointment.Status = AppointmentStatus.Failed;
                _unitOfWork.Repository<Appointment>().Update(appointment);

                var schedule = appointment.PsychologistSchedule;
                schedule.Status = PsychologistScheduleStatus.Free;
                _unitOfWork.Repository<PsychologistSchedule>().Update(schedule);

                await _unitOfWork.CompleteAsync();

                return (appointment, schedule);
            }
        }
    }
}
