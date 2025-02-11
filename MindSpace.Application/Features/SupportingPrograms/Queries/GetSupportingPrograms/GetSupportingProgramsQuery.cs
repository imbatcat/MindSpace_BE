using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingPrograms
{
    public class GetSupportingProgramsQuery : IRequest<PagedResultDTO<SupportingProgramResponseDTO>>
    {
        // ================================
        // === Fields & Props
        // ================================

        public SupportingProgramSpecParams SpecParams { get; private set; }

        // ================================
        // === Constructors
        // ================================

        public GetSupportingProgramsQuery(SupportingProgramSpecParams specParams)
        {
            this.SpecParams = specParams;
        }
    }
}