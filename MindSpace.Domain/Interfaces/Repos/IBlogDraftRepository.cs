using MindSpace.Domain.Entities.Drafts.Blog;

namespace MindSpace.Domain.Interfaces.Repos
{
    public interface IBlogDraftRepository
    {
        Task<BlogDraft?> GetBlogDraftAsync(string blogDraftId);
        Task<BlogDraft?> SetBlogDraftAsync(BlogDraft blogDraft);
        Task<bool> DeleteBlogDraftAsync(string blogDraftId);
    }
}
