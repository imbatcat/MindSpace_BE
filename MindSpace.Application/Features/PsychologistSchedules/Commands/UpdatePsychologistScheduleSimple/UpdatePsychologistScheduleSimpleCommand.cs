using MediatR;
using MindSpace.Application.DTOs.Appointments;

namespace MindSpace.Application.Features.PsychologistSchedules.Commands.UpdatePsychologistScheduleSimple
{
    public class UpdatePsychologistScheduleSimpleCommand : IRequest
    {
        public int PsychologistId { get; set; }
        public DateOnly? StartDate { get; set; } //FE must pass this always Monday
        public DateOnly? EndDate { get; set; } //FE must pass this always Sunday
        public required List<TimeSlotDTO> Timeslots { get; set; }
    }
}
