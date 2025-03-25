using MediatR;
using MindSpace.Application.DTOs.Tests;

namespace MindSpace.Application.Features.TestResponses.Queries.GetTestResponseById
{
    public class GetTestResponseByIdQuery : IRequest<TestResponseResponseDTO>
    {
        public int Id { get; set; }
        public GetTestResponseByIdQuery(int id)
        {
            Id = id;
        }
    }
}
