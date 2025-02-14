using MindSpace.Domain.Entities.Drafts.TestPeriodics;
using MindSpace.Domain.Interfaces.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace MindSpace.Application.Services
{
    public class TestDraftService : ITestDraftService
    {
        // ==================================
        // === Fields & Props
        // ==================================

        private readonly IDatabase _database;

        // ==================================
        // === Constructors
        // ==================================

        public TestDraftService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        // ==================================
        // === Methods
        // ==================================

        public async Task<bool> DeleteTestDraftAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<TestDraft?> GetTestDraftAsync(string key)
        {
            var testDraft = await _database.StringGetAsync(key);
            return testDraft.IsNullOrEmpty ? null : JsonSerializer.Deserialize<TestDraft>(testDraft);
        }

        public async Task<TestDraft?> SetTestDraftAsync(TestDraft testDraft)
        {
            // Object maintains only 2 days
            var created = await _database.StringSetAsync(testDraft.Id,
                JsonSerializer.Serialize(testDraft),
                TimeSpan.FromDays(2));

            if (!created) return null;
            return await GetTestDraftAsync(testDraft.Id);
        }
    }
}
