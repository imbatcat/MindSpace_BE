using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.Helpers.Requests;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Specifications;

namespace MindSpace.API.Controllers
{
    [ApiVersion(1)]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {
    }
}
