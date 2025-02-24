using MediatR;
using MindSpace.Application.DTOs.Appointments;

namespace MindSpace.Application.Features.PsychologistSchedules.Commands
{
    public class UpdatePsychologistScheduleCommand : IRequest
    {
        public int PsychologistId { get; set; }
        public DateOnly StartDate { get; set; } // always Monday
        public DateOnly EndDate { get; set; } // always Sunday
        public List<PsychologistScheduleResponseDTO> psychologistScheduleResponseDTOs { get; set; } // list slots for each week day
    }
}
