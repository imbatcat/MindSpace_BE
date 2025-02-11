using MindSpace.Domain.Entities.Drafts.TestPeriodic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Interfaces.Services
{
    public interface ITestDraftService
    {
        Task<TestDraft?> GetTestDraftAsync(string key);
        Task<TestDraft?> SetTestDraftAsync(TestDraft testDraft);
        Task<bool> DeleteTestDraftAsync(string key);
    }
}
