using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;
using StackExchange.Redis;
using System.Text.Json;

namespace MindSpace.Infrastructure.Services
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
            _database = redis.GetDatabase(0);
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
