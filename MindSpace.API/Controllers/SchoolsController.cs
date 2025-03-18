using Microsoft.AspNetCore.Mvc;
using MediatR;
using MindSpace.Application.Features.Schools.Queries.ViewAllSchools;
using MindSpace.Application.DTOs;
using MindSpace.API.RequestHelpers;

namespace MindSpace.API.Controllers
{
    public class SchoolsController(
        IMediator mediator
    ) : BaseApiController
    {
        [Cache(30000)]
        [HttpGet]
        public async Task<ActionResult<List<SchoolDTO>>> GetAllSchools()
        {
            var schools = await mediator.Send(new ViewAllSchoolsQuery());
            return Ok(schools);
        }
    }
}