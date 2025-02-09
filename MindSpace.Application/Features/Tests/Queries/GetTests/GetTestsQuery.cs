using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Specifications.TestSpecifications;

namespace MindSpace.Application.Features.Tests.Queries.GetTests
{
    public class GetTestsQuery : IRequest<PagedResultDTO<TestResponseDTO>>
    {
        public TestSpecParams SpecParams { get; private set; }

        public GetTestsQuery(TestSpecParams specParams)
        {
            this.SpecParams = specParams;
        }
    }
}