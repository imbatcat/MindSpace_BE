using MediatR;
using MindSpace.Application.Features.SupportingPrograms.Specifications;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetCountSupportingPrograms
{
    public class GetCountSupportingProgramsQuery : IRequest<int>
    {
        public SupportingProgramSpecParams SpecParams { get; private set; }

        public GetCountSupportingProgramsQuery(SupportingProgramSpecParams specParams)
        {
            SpecParams = specParams;
        }
    }
}
