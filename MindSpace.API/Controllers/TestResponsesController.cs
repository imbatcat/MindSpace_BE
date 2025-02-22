using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.TestResponses.Queries.GetTestResponses;
using MindSpace.Application.Specifications.TestResponseSpecifications;

namespace MindSpace.API.Controllers
{
    public class TestResponsesController : BaseApiController
    {
        //============================
        // props and fields
        //============================

        private readonly IMediator _mediator;

        // ============================
        // Constructors
        // ============================
        public TestResponsesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ============================
        // GET
        // ============================
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TestResponseOverviewResponseDTO>>> GetTestResponses([FromQuery] TestResponseSpecParams specParams)
        {
            var data = await _mediator.Send(new GetTestResponsesQuery(specParams));
            return PaginationOkResult<TestResponseOverviewResponseDTO>(
                    data.Data, 
                    data.Count, 
                    specParams.PageIndex, 
                    specParams.PageSize
                );
        }
    }
}
