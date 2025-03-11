using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Drafts.Blogs;
using StackExchange.Redis;
using System.Text.Json;

namespace MindSpace.Infrastructure.Services.CachingServices
{
    public class BlogDraftService : IBlogDraftService
    {
        // ====================================
        // === Props & Fields
        // ====================================

        private readonly IDatabase _database;

        // ====================================
        // === Constructors
        // ====================================

        public BlogDraftService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        // ====================================
        // === Methods
        // ====================================

        public async Task<bool> DeleteBlogDraftAsync(string blogDraftId)
        {
            return await _database.KeyDeleteAsync(blogDraftId);
        }

        public async Task<BlogDraft?> GetBlogDraftAsync(string blogDraftId)
        {
            var blogDraft = await _database.StringGetAsync(blogDraftId);
            return blogDraft.IsNullOrEmpty ? null : JsonSerializer.Deserialize<BlogDraft>(blogDraft);
        }

        public async Task<BlogDraft?> SetBlogDraftAsync(BlogDraft blogDraft)
        {
            var IsSetSuccessful = await _database.StringSetAsync(blogDraft.Id,
                JsonSerializer.Serialize(blogDraft),
                TimeSpan.FromHours(2));

            return !IsSetSuccessful ? null : await GetBlogDraftAsync(blogDraft.Id);
        }
    }
}
