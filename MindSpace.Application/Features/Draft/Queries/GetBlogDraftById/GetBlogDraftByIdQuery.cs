using MediatR;
using MindSpace.Domain.Entities.Drafts.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.Draft.Queries.GetBlogDraftById
{
    public class GetBlogDraftByIdQuery : IRequest<BlogDraft>
    {
        public string Id { get; private set; }

        public GetBlogDraftByIdQuery(string id)
        {
            Id = id;
        }
    }
}
