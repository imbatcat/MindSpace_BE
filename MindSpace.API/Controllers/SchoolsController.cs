using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs;
using MindSpace.Application.Features.Schools.Queries.ViewAllSchools;

namespace MindSpace.API.Controllers
{
    public class SchoolsController(
        IMediator mediator
    ) : BaseApiController
    {
        //[Cache(30000)]
        [HttpGet]
        public async Task<ActionResult<List<SchoolDTO>>> GetAllSchools()
        {
            var schools = await mediator.Send(new ViewAllSchoolsQuery());
            return Ok(schools);
        }
    }
}