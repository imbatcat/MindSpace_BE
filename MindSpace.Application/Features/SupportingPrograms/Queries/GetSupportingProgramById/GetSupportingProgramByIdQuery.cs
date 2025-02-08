using MediatR;
using MindSpace.Domain.Entities.SupportingPrograms;

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
