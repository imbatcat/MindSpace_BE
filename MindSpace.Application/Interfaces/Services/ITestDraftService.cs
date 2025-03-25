using MindSpace.Domain.Entities.Drafts.TestPeriodics;

namespace MindSpace.Application.Interfaces.Services
{
    public interface ITestDraftService
    {
        Task<TestDraft?> GetTestDraftAsync(string testDraftId);
        Task<TestDraft?> SetTestDraftAsync(TestDraft testDraft);
        Task<bool> DeleteTestDraftAsync(string testDraftId);
    }
}
