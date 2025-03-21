using MediatR;
using MindSpace.Application.DTOs.Tests;

namespace MindSpace.Application.Features.Tests.Queries.GetMostRecentTests
{
    public class GetMostRecentTestsQuery : IRequest<List<TestOverviewResponseDTO>>
    {
        public int SchoolId { get; set; }
        public int Top {  get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; } 
    }
}

