using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.ViewAllPsychologists
{
    public class ViewAllPsychologistsQuery : IRequest<List<string>>
    {
    }
}
