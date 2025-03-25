using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Application.Specifications.SupportingProgramHistorySpecifications;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramByHistory
{
    public class GetSupportingProgramByHistoryQuery : IRequest<PagedResultDTO<SupportingProgramResponseDTO>>
    {
        // ================================
        // === Fields & Props
        // ================================

        public SupportingProgramHistorySpecParams SpecParams { get; private set; }

        public GetSupportingProgramByHistoryQuery(SupportingProgramHistorySpecParams specParams)
        {
            SpecParams = specParams;
        }
    }
}
