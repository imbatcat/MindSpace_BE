using MediatR;
using MindSpace.Application.DTOs.SupportingPrograms;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramById
{
    public class GetSupportingProgramByIdQuery : IRequest<SupportingProgramSingleResponseDTO>
    {
        // ================================
        // === Fields & Props
        // ================================
        public int Id { get; private set; }

        // ================================
        // === Constructors
        // ================================

        public GetSupportingProgramByIdQuery(int id)
        {
            Id = id;
        }
    }
}