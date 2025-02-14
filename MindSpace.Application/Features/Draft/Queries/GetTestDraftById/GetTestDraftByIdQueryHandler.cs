using MediatR;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;
using MindSpace.Domain.Interfaces.Services;

namespace MindSpace.Application.Features.Draft.Queries.GetTestDraftById
{
    public class GetTestDraftByIdQueryHandler : IRequestHandler<GetTestDraftByIdQuery, TestDraft>
    {
        // ====================================
        // === Props & Fields
        // ====================================
        public readonly ITestDraftService _testDraftService;

        // ====================================
        // === Constructors
        // ====================================

        public GetTestDraftByIdQueryHandler(ITestDraftService testDraftService)
        {
            _testDraftService = testDraftService;
        }

        // ====================================
        // === Methods
        // ====================================
        public async Task<TestDraft> Handle(GetTestDraftByIdQuery request, CancellationToken cancellationToken)
        {
            var testDraft = await _testDraftService.GetTestDraftAsync(request.Id);
            return testDraft ?? new TestDraft() { Id = request.Id };
        }
    }
}
