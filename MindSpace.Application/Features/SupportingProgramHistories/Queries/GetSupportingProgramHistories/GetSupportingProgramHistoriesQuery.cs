using MediatR;
using MindSpace.Application.Features.SupportingProgramHistories.Specifications;
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
