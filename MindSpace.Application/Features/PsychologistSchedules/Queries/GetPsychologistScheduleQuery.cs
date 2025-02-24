using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Specifications.PsychologistScheduleSpecifications;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.PsychologistSchedules.Queries
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
