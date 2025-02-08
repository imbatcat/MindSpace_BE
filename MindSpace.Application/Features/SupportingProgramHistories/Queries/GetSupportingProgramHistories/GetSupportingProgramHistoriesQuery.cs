using MediatR;
using MindSpace.Application.Specifications.SupportingProgramHistory;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.Application.Features.SupportingProgramHistories.Queries.GetSupportingProgramHistories
{
    public class GetSupportingProgramHistoriesQuery : IRequest<IReadOnlyList<SupportingProgramHistory>>
    {
        public SupportingProgramHistorySpecParams SpecParams { get; private set; }

        public GetSupportingProgramHistoriesQuery(SupportingProgramHistorySpecParams specParams)
        {
            SpecParams = specParams;
        }
    }
}
