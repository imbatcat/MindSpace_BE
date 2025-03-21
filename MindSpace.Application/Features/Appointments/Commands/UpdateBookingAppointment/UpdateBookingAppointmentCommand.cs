using MediatR;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.Appointments.Commands.UpdateBookingAppointment
{
    public class UpdateBookingAppointmentCommand : IRequest<Appointment>
    {
        public required Appointment Appointment { get; set; }
    }
}