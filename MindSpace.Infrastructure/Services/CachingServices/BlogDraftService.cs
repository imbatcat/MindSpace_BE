using MindSpace.Application.Commons.Constants;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Drafts.Blogs;
using StackExchange.Redis;
using System.Text.Json;

namespace MindSpace.Infrastructure.Services.CachingServices
{
    public class BlogDraftService(IConnectionMultiplexer redisMul) : IBlogDraftService
    {
        // ====================================
        // === Props & Fields
        // ====================================

        private readonly IDatabase _redisDb = redisMul.GetDatabase(AppCts.Redis.DatabaseNo_Blog);
        private const int EXPIRATION_DAYS = 2;

        // ====================================
        // === Methods
        // ====================================

        public async Task<bool> DeleteBlogDraftAsync(string blogDraftId)
        {
            return await _redisDb.KeyDeleteAsync(blogDraftId);
        }

        public async Task<BlogDraft?> GetBlogDraftAsync(string blogDraftId)
        {
            var blogDraft = await _redisDb.StringGetAsync(blogDraftId);
            return blogDraft.IsNullOrEmpty ? null : JsonSerializer.Deserialize<BlogDraft>(blogDraft);
        }

        public async Task<BlogDraft?> SetBlogDraftAsync(BlogDraft blogDraft)
        {
            var IsSetSuccessful = await _redisDb.StringSetAsync(blogDraft.Id,
                JsonSerializer.Serialize(blogDraft),
                TimeSpan.FromDays(EXPIRATION_DAYS));

            return !IsSetSuccessful ? null : await GetBlogDraftAsync(blogDraft.Id);
        }
    }
}
