using MediatR;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
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
