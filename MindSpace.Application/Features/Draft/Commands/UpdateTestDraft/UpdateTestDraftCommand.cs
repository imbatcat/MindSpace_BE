using MediatR;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;

namespace MindSpace.Application.Features.Draft.Commands.UpdateTestDraft
{
    public class UpdateTestDraftCommand : IRequest<TestDraft>
    {
        public TestDraft TestDraft { get; }

        public UpdateTestDraftCommand(TestDraft testDraft)
        {
            TestDraft = testDraft;
        }
    }
}
