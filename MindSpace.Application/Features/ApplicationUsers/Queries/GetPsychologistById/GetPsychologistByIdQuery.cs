using MediatR;
using MindSpace.Application.DTOs.ApplicationUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.GetPsychologistById
{
    public class GetPsychologistByIdQuery : IRequest<PsychologistProfileDTO>
    {
        public int Id { get; private set; }
        public GetPsychologistByIdQuery(int id)
        {
            Id = id;
        }
    }
}
