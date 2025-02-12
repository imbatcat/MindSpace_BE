using MindSpace.Domain.Entities.Drafts.TestPeriodic;

namespace MindSpace.Domain.Interfaces.Services
{
    public interface ITestDraftService
    {
        Task<TestDraft?> GetTestDraftAsync(string testDraftId);
        Task<TestDraft?> SetTestDraftAsync(TestDraft testDraft);
        Task<bool> DeleteTestDraftAsync(string testDraftId);
    }
}
