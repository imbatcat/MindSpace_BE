using MindSpace.Domain.Entities.Drafts.TestPeriodic;
using MindSpace.Domain.Interfaces.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace MindSpace.Application.Services
{
    public class TestPeriodictDraftService : ITestDraftService
    {
        private readonly IDatabase _database;

        public TestPeriodictDraftService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteTestDraftAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<TestDraft?> GetTestDraftAsync(string key)
        {
            var data = await _database.StringGetAsync(key);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<TestDraft>(data);
        }

        public async Task<TestDraft?> SetTestDraftAsync(TestDraft testDraft)
        {
            var created = await _database.StringSetAsync(testDraft.Id,
                JsonSerializer.Serialize(testDraft),
                TimeSpan.FromDays(2));

            if (!created) return null;
            return await GetTestDraftAsync(testDraft.Id);
        }
    }
}
