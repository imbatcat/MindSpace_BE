using MindSpace.Domain.Entities.Drafts.TestPeriodic;

namespace MindSpace.Domain.Interfaces.Repos
{
    public interface ITestDraftRepository
    {
        Task<TestDraft?> GetTestDraftAsync(string testDraftId);
        Task<TestDraft?> SetTestDraftAsync(TestDraft testDraft);
        Task<bool> DeleteTestDraftAsync(string testDraftId);
    }
}
