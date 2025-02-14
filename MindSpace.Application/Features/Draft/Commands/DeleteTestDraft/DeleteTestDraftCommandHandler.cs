using MediatR;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Services;

namespace MindSpace.Application.Features.Draft.Commands.DeleteTestDraft
{
    public class DeleteTestDraftCommandHandler : IRequestHandler<DeleteTestDraftCommand>
    {
        private readonly ITestDraftService _testDraftService;

        public DeleteTestDraftCommandHandler(ITestDraftService testDraftService)
        {
            _testDraftService = testDraftService;
        }

        public async Task Handle(DeleteTestDraftCommand request, CancellationToken cancellationToken)
        {
            var isDeletedSuccessful = await _testDraftService.DeleteTestDraftAsync(request.Id);

            if (!isDeletedSuccessful) throw new NotFoundException(nameof(TestDraft), request.Id);
        }
    }
}
