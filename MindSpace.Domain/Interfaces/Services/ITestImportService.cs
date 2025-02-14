using Microsoft.AspNetCore.Http;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Domain.Interfaces.Services
{
    public interface ITestImportService
    {
        Task<Dictionary<string, List<Dictionary<string, string>>>> ReadAndValidateTestFileAsync(IFormFile file);
        void InsertPsychologyTestOptions(Test testEntity, List<Dictionary<string, string>> optionsData);
        void InsertQuestions(Test testEntity, List<Dictionary<string, string>> questionData);
        void InsertScoreRanks(Test testEntity, List<Dictionary<string, string>> scoreRankData);

    }
}
