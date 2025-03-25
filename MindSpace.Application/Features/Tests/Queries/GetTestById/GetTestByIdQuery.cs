
using MediatR;
using MindSpace.Application.DTOs.Tests;

namespace MindSpace.Application.Features.Tests.Queries.GetTestById
{
    public class GetTestByIdQuery : IRequest<TestResponseDTO>
    {
        public int Id { get; private set; }
        public GetTestByIdQuery(int id)
        {
            Id = id;
        }
    }
}
