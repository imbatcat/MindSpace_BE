using MediatR;
using MindSpace.Application.DTOs.Appointments;

namespace MindSpace.Application.Features.PsychologistSchedules.Commands.UpdatePsychologistScheduleSimple
{
    public class UpdatePsychologistScheduleSimpleCommand : IRequest
    {
        public int PsychologistId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public required List<TimeSlotDTO> Timeslots { get; set; }
    }
}
