using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.Specifications.SupportingProgramHistorySpecifications;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramByHistory
{
    public class GetSupportingProgramHistoryQuery : IRequest<PagedResultDTO<SupportingProgramResponseDTO>>
    {
        public SupportingProgramHistorySpecParams SpecParams { get; private set; }

        public GetSupportingProgramHistoryQuery(SupportingProgramHistorySpecParams specParams)
        {
            SpecParams = specParams;
        }
    }
}
