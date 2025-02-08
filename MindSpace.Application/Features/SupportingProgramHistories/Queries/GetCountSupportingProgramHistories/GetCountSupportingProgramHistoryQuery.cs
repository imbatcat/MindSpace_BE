using MediatR;
using MindSpace.Application.Specifications.SupportingProgramHistory;

namespace MindSpace.Application.Features.SupportingProgramHistories.Queries.GetCountSupportingProgramHistories
{
    public class GetCountSupportingProgramHistoryQuery : IRequest<int>
    {
        public SupportingProgramHistorySpecParams SpecParams { get; private set; }

        public GetCountSupportingProgramHistoryQuery(SupportingProgramHistorySpecParams specParams)
        {
            SpecParams = specParams;
        }
    }
}
