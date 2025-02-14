using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.Resources.Commands.CreateResourceAsBlog
{
    public class CreateResourceAsBlogCommand : IRequest
    {
        public string BlogDraftId { get; private set; }

        public CreateResourceAsBlogCommand(string blogDraftId)
        {
            BlogDraftId = blogDraftId;
        }
    }
}
