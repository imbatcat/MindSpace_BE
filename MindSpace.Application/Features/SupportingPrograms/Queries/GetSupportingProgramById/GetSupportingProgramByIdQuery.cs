using MediatR;
using MindSpace.Application.DTOs;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramById
{
    public class GetSupportingProgramByIdQuery : IRequest<SupportingProgramWithStudentsResponseDTO>
    {
        public int Id { get; private set; }

        public GetSupportingProgramByIdQuery(int id)
        {
            Id = id;
        }
    }
}