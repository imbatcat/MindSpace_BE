using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramById
{
    public class GetSupportingProgramByIdQuery : IRequest<SupportingProgramDTO>
    {
        public int Id { get; private set; }

        public GetSupportingProgramByIdQuery(int id)
        {
            Id = id;
        }
    }
}
