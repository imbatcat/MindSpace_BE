using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Specifications.PsychologistsSpecifications;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.GetAllPsychologists
{
    public class GetAllPsychologistsQuery : IRequest<PagedResultDTO<PsychologistProfileDTO>>
    {
        public PsychologistSpecParams SpecParams { get; private set; }

        public GetAllPsychologistsQuery(PsychologistSpecParams specParams)
        {
            SpecParams = specParams;
        }
    }
}