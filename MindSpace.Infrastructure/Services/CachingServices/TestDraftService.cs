using MindSpace.Application.Commons.Constants;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;
using StackExchange.Redis;
using System.Text.Json;

namespace MindSpace.Infrastructure.Services.CachingServices
{
    public class TestDraftService(IConnectionMultiplexer redisMul) : ITestDraftService
    {
        // ==================================
        // === Fields & Props
        // ==================================

        private readonly IDatabase _redisDb = redisMul.GetDatabase(AppCts.Redis.DatabaseNo_Test);
        private const int EXPIRATION_DAYS = 2;

        // ==================================
        // === Methods
        // ==================================

        public async Task<bool> DeleteTestDraftAsync(string key)
        {
            return await _redisDb.KeyDeleteAsync(key);
        }

        public async Task<TestDraft?> GetTestDraftAsync(string key)
        {
            var testDraftJsonObject = await _redisDb.StringGetAsync(key);
            return testDraftJsonObject.IsNullOrEmpty ? null : JsonSerializer.Deserialize<TestDraft>(testDraftJsonObject);
        }

        public async Task<TestDraft?> SetTestDraftAsync(TestDraft testDraft)
        {
            // Object maintains only 2 days
            var IsSetSuccessful = await _redisDb.StringSetAsync(testDraft.Id,
                JsonSerializer.Serialize(testDraft),
                TimeSpan.FromDays(EXPIRATION_DAYS));

            return !IsSetSuccessful ? null : await GetTestDraftAsync(testDraft.Id);
        }
    }
}
