using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.Helpers.Requests;

namespace MindSpace.API.Controllers
{
    [ApiVersion(1)]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult PaginationOkResult<T>(IReadOnlyList<T> items, int count, int pageIndex, int pageSize)
        {
            var pagination = new Pagination<T>(pageIndex, pageSize, count, items);
            return Ok(pagination);
        }
    }
}
