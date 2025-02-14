using MediatR;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Services;

namespace MindSpace.Application.Features.Draft.Commands.UpdateTestDraft
{
    public class UpdateTestDraftCommandHandler : IRequestHandler<UpdateTestDraftCommand, TestDraft>
    {
        private readonly ITestDraftService _testDraftService;

        public UpdateTestDraftCommandHandler(ITestDraftService testDraftService)
        {
            _testDraftService = testDraftService;
        }

        public async Task<TestDraft> Handle(UpdateTestDraftCommand request, CancellationToken cancellationToken)
        {
            var updatedTestDraft = await _testDraftService.SetTestDraftAsync(request.TestDraft)
                ?? throw new NotFoundException(nameof(TestDraft), request.TestDraft.Id);

            return updatedTestDraft;
        }
    }
}
