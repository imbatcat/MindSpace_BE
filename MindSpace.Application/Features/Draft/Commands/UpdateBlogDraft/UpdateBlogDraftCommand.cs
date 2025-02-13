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
        public BlogDraft BlogDraft { get; }

        public UpdateBlogDraftCommand(BlogDraft blogDraft)
        {
            BlogDraft = blogDraft;
        }
    }
}
