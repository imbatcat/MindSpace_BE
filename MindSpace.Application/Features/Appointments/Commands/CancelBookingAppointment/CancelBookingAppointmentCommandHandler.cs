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

namespace MindSpace.Application.Features.Appointments.Commands.CancelBookingAppointment;

public class CancelBookingAppointmentCommandHandler(
    IUnitOfWork unitOfWork,
    ILogger<CancelBookingAppointmentCommandHandler> logger,
    INotificationService notificationService,
    IConfiguration configuration,
    IMapper mapper) : IRequestHandler<CancelBookingAppointmentCommand>
{
    public async Task Handle(CancelBookingAppointmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Cancelling booking appointment for student {StudentId}", request.StudentId);

            var (appointment, schedule) = await UpdateAppointemntAndScheduleAsync(request.StudentId, request.PsychologistId, request.ScheduleId);

            // Notify schedule free to both student and psychologist
            await notificationService.NotifyPsychologistScheduleFree(
                UserRoles.Student,
                mapper.Map<PsychologistScheduleNotificationResponseDTO>(schedule));

            await notificationService.NotifyPsychologistScheduleFree(
                UserRoles.Psychologist,
                mapper.Map<PsychologistScheduleNotificationResponseDTO>(schedule));

            logger.LogInformation("Successfully cancelled booking appointment for student {StudentId}", request.StudentId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error cancelling booking appointment for student {StudentId}", request.StudentId);
            throw;
        }

        async Task<(Appointment, PsychologistSchedule)> UpdateAppointemntAndScheduleAsync(int studentId, int psychologistId, int scheduleId)
        {
            var sessionExpirationMinutes = configuration.GetValue<int>("Stripe:SessionExpirationMinutes");
            var appointmentSpecification = new AppointmentSpecification(request.StudentId, request.PsychologistId, request.ScheduleId);
            var appointment = await unitOfWork.Repository<Appointment>().GetBySpecAsync(appointmentSpecification)
                ?? throw new NotFoundException(nameof(Appointment), request.StudentId.ToString());

            appointment.Status = AppointmentStatus.Failed;
            unitOfWork.Repository<Appointment>().Update(appointment);

            var schedule = appointment.PsychologistSchedule;
            schedule.Status = PsychologistScheduleStatus.Free;
            unitOfWork.Repository<PsychologistSchedule>().Update(schedule);

            await unitOfWork.CompleteAsync();

            return (appointment, schedule);
        }
    }
}
