using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.Specifications.SpecializationSpecifications;

namespace MindSpace.Application.Features.Specializations.Queries
{
    public class GetSpecializationsQuery : IRequest<PagedResultDTO<SpecializationDTO>>
    {
        public SpecializationSpecParams SpecParams { get; private set; }

        // ================================
        // === Constructors
        // ================================

        public GetSpecializationsQuery(SpecializationSpecParams specParams)
        {
            this.SpecParams = specParams;
        }
    }
}
