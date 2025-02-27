using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services.SignalR;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Appointments.Commands.CancelBookingAppointment
{
    public class CancelBookingAppointmentCommandHandler : IRequestHandler<CancelBookingAppointmentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CancelBookingAppointmentCommandHandler> _logger;
        private readonly ISignalRNotification _signalRNotification;
        private readonly IConfiguration _configuration;

        public CancelBookingAppointmentCommandHandler(IUnitOfWork unitOfWork, ILogger<CancelBookingAppointmentCommandHandler> logger, ISignalRNotification signalRNotification, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _signalRNotification = signalRNotification;
            _configuration = configuration;
        }

        public async Task Handle(CancelBookingAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Cancelling booking appointment for student {StudentId}", request.StudentId);

                var (appointment, schedule) = await UpdateAppointemntAndScheduleAsync(request.StudentId, request.PsychologistId, request.ScheduleId);

                await _signalRNotification.NotifyScheduleStatus(schedule);

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
