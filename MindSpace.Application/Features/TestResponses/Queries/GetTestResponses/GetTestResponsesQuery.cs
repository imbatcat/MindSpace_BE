using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Specifications.TestResponseSpecifications;

namespace MindSpace.Application.Features.TestResponses.Queries.GetTestResponses
{
    public class GetTestResponsesQuery : IRequest<PagedResultDTO<TestResponseOverviewResponseDTO>>
    {
        public TestResponseSpecParams SpecParams { get; set; }
        public GetTestResponsesQuery(TestResponseSpecParams specParams)
        {
            SpecParams = specParams;
        }
    }
}
