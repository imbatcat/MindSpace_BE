using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        // Using database no.1 for isolation
        private const int DATABASE_NO = 1;
        private readonly IDatabase _cacheDatabase = redisConn.GetDatabase(DATABASE_NO);

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
                database: DATABASE_NO,
                pattern: $"*{pattern}*").ToArray();

            if (cacheKeys.Length != 0)
            {
                await _cacheDatabase.KeyDeleteAsync(cacheKeys);
            }
        }
    }
}
