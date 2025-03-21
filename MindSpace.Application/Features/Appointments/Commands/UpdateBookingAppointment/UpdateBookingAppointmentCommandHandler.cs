using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.Appointments.Commands.UpdateBookingAppointment
{
    public class UpdateBookingAppointmentCommandHandler(
        IUnitOfWork _unitOfWork,
        ILogger<UpdateBookingAppointmentCommandHandler> _logger
    ) : IRequestHandler<UpdateBookingAppointmentCommand, Appointment>
    {
        public async Task<Appointment> Handle(UpdateBookingAppointmentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating booking appointment {AppointmentId}", request.Appointment.Id);
            var updatedAppointment = _unitOfWork.Repository<Appointment>().Update(request.Appointment);

            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Booking appointment {AppointmentId} updated", request.Appointment.Id);

            return updatedAppointment;
        }
    }
}