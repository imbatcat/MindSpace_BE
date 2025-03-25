using MediatR;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Specifications.PsychologistScheduleSpecifications;

namespace MindSpace.Application.Features.PsychologistSchedules.Queries.GetPsychologistSchedule
{
    public class GetPsychologistScheduleQuery : IRequest<IReadOnlyList<PsychologistScheduleResponseDTO>>
    {
        public PsychologistScheduleSpecParams SpecParams { get; set; }
        public GetPsychologistScheduleQuery(PsychologistScheduleSpecParams specParams)
        {
            SpecParams = specParams;
        }
    }
}
