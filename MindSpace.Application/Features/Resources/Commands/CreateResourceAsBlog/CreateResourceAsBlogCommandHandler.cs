using AutoMapper;
using MediatR;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Resources.Commands.CreateResourceAsBlog;

public class CreateResourceAsBlogCommandHandler(
    IMapper mapper,
    IBlogDraftService blogDraftService,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateResourceAsBlogCommand, BlogResponseDTO>
{
    public async Task<BlogResponseDTO> Handle(CreateResourceAsBlogCommand request, CancellationToken cancellationToken)
    {
        var blogDraft = await blogDraftService.GetBlogDraftAsync(request.BlogDraftId);

        // Check each field in the blog draft to see any missing data
        if (blogDraft == null) throw new NotFoundException(nameof(BlogDraft), request.BlogDraftId);

        // Add blog to table
        var blogToAdd = mapper.Map<BlogDraft, Resource>(blogDraft);

        // Commit all changes
        var addedBlog = unitOfWork.Repository<Resource>().Insert(blogToAdd)
            ?? throw new CreateFailedException("Blog");
        await unitOfWork.CompleteAsync();

        // Remove blog draft from redis
        await blogDraftService.DeleteBlogDraftAsync(blogDraft.Id);

        return mapper.Map<Resource, BlogResponseDTO>(addedBlog);
    }
}





