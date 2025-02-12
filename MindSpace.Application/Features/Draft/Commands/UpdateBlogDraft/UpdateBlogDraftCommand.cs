using MediatR;
using MindSpace.Domain.Entities.Drafts.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.Draft.Commands.UpdateBlogDraft
{
    public class UpdateBlogDraftCommand : IRequest<BlogDraft>
    {
        public string Id { get; }
        public BlogDraft BlogDraft { get; }

        public UpdateBlogDraftCommand(string id, BlogDraft blogDraft)
        {
            Id = id;
            BlogDraft = blogDraft;
        }
    }
}
