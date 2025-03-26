using MediatR;
using MindSpace.Application.DTOs.ApplicationUsers;

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
