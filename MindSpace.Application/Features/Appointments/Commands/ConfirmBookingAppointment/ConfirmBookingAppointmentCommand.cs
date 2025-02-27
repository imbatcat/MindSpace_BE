using MediatR;
using MindSpace.Application.DTOs.Appointments;

namespace MindSpace.Application.Features.Appointments.Commands.ConfirmBookingAppointment
{
    public class ConfirmBookingAppointmentCommand : IRequest<ConfirmBookingAppointmentResult>
    {
        public int PsychologistId { get; set; }
        public int ScheduleId { get; set; }
        public int StudentId { get; set; }
        public int SpecializationId { get; set; }

    }
}