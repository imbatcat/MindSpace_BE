using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MindSpace.API.Controllers
{
    [Route("api/v{version:apiVersion}/psychologists")]
    public class PsychologistsController(IMediator mediator) : BaseApiController
    {
        // GET list by filter

        // GET psychologist details by id
    }
}
