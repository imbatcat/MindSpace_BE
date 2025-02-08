using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingPrograms
{
    public class GetSupportingProgramsQuery : IRequest<PagedResultDTO<SupportingProgramDTO>>
    {
        public SupportingProgramSpecParams SpecParams { get; private set; }

        public GetSupportingProgramsQuery(SupportingProgramSpecParams specParams)
        {
            this.SpecParams = specParams;
        }
    }
}