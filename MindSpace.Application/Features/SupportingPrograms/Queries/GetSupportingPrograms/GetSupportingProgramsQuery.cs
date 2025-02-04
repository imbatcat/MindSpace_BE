using MediatR;
using MindSpace.Application.Features.SupportingPrograms.Specifications;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingPrograms
{
    public class GetSupportingProgramsQuery : IRequest<IReadOnlyList<SupportingProgram>>
    {
        public SupportingProgramSpecParams SpecParams { get; private set; }

        public GetSupportingProgramsQuery(SupportingProgramSpecParams specParams)
        {
            this.SpecParams = specParams;
        }
    }
}
