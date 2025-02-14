using MediatR;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;

namespace MindSpace.Application.Features.Draft.Queries.GetTestDraftById
{
    public class GetTestDraftByIdQuery : IRequest<TestDraft>
    {
        public string Id { get; private set; }
        public GetTestDraftByIdQuery(string id)
        {
            Id = id;
        }
    }
}
