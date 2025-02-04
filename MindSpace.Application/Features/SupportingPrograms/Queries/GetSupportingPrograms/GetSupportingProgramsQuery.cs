using MediatR;
using MindSpace.Application.Features.SupportingPrograms.Specs;
using MindSpace.Domain.Entities.SupportingPrograms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingPrograms
{
    public class GetSupportingProgramsQuery : IRequest<IReadOnlyList<SupportingProgram>>
    {
        public SupportingProgramSpecParams specParams { get; private set; }

        public GetSupportingProgramsQuery(SupportingProgramSpecParams specParams)
        {
            this.specParams = specParams;
        }
    }
}
