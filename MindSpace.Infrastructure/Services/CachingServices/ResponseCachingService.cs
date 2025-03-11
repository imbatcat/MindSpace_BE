using MindSpace.Application.Commons.Constants;
using MindSpace.Application.Interfaces.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace MindSpace.Infrastructure.Services.CachingServices
{
    // Using the architecture of simple Redis Server setup
    /*

         Application ─► ConnectionMultiplexer ─► Single Redis Server
                                             └── Database 1
     */
    public class ResponseCachingService(IConnectionMultiplexer redisConn) : IResponseCachingService
    {
        // ==============================
        // === Fields & Props
        // ==============================

        // Using database no.3 for isolation
        private readonly IDatabase _cacheDatabase = redisConn.GetDatabase(AppCts.Redis.DatabaseNo_Response);

        // ==============================
        // === Constructors
        // ==============================

        public async Task StoreDataInCacheAsync(string key, object responseData, TimeSpan expirationTime)
        {
            var serializedResponse = JsonSerializer.Serialize(responseData, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            // Set the serialized object with the given key
            await _cacheDatabase.StringSetAsync(
                new RedisKey(key),
                new RedisValue(serializedResponse),
                expirationTime
            );
        }

        public async Task<string?> GetDataFromCacheAsync(string key)
        {
            // indended json
            var result = await _cacheDatabase.StringGetAsync(
                new RedisKey(key));

            if (result.IsNullOrEmpty) return null;
            return result;
        }

        public async Task RemoveDataInCacheByPatternAsync(string pattern)
        {
            // Get the current server of the Redis
            var server = redisConn.GetServer(redisConn.GetEndPoints().First());

            // Find all keys having the same pattern
            var cacheKeys = server.Keys(
                database: AppCts.Redis.DatabaseNo_Response,
                pattern: $"*{pattern}*").ToArray();

            if (cacheKeys.Length != 0)
            {
                await _cacheDatabase.KeyDeleteAsync(cacheKeys);
            }
        }
    }
}
