using MediatR;
using MindSpace.Domain.Entities.SupportingPrograms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramById
{
    public class GetSupportingProgramByIdQuery : IRequest<SupportingProgram>
    {
        public int Id { get; private set; }

        public GetSupportingProgramByIdQuery(int id)
        {
            Id = id;
        }
    }
}
