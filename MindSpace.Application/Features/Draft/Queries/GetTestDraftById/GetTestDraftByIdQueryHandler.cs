using MediatR;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;

namespace MindSpace.Application.Features.Draft.Queries.GetTestDraftById;

public class GetTestDraftByIdQueryHandler(ITestDraftService testDraftService) : IRequestHandler<GetTestDraftByIdQuery, TestDraft>
{
    public async Task<TestDraft> Handle(GetTestDraftByIdQuery request, CancellationToken cancellationToken)
    {
        var testDraft = await testDraftService.GetTestDraftAsync(request.Id);
        return testDraft ?? new TestDraft() { Id = request.Id };
    }
}
