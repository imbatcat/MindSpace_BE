using MediatR;

namespace MindSpace.Application.Features.Appointments.Commands.CancelBookingAppointment
{
    public class CancelBookingAppointmentCommand : IRequest
    {
        public int StudentId { get; set; }
        public int PsychologistId { get; set; }
        public int ScheduleId { get; set; }
    }
}
