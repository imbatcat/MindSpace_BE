using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.Draft.Commands.DeleteBlogDraft
{
    public class DeleteBlogDraftCommand : IRequest
    {
        public string Id { get; }

        public DeleteBlogDraftCommand(string id)
        {
            Id = id;
        }
    }
}
