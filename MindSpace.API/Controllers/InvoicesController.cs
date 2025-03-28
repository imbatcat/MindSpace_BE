using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.Features.Invoices.Queries.GetInvoiceHistoryByUser;
using MindSpace.Application.Features.Invoices.Queries.GetInvoiceList;
using MindSpace.Application.Specifications.InvoicesSpecifications;

namespace MindSpace.API.Controllers
{
    [Route("api/v{version:apiVersion}/invoices")]
    public class InvoicesController(IMediator mediator) : BaseApiController
    {
        // GET /api/invoices/user
        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetInvoiceHistoryByUser([FromQuery] InvoiceSpecParams specParams)
        {
            var result = await mediator.Send(new GetInvoiceHistoryByUserQuery(specParams));
            return PaginationOkResult(
                result.Data,
                result.Count,
                specParams.PageIndex,
                specParams.PageSize
            );
        }

        // get invoice list by params
        // GET /api/invoices
        [HttpGet()]
        public async Task<IActionResult> GetInvoiceList([FromQuery] InvoiceSpecParams specParams)
        {
            var result = await mediator.Send(new GetInvoiceListQuery(specParams));
            return PaginationOkResult(
                result.Data,
                result.Count,
                specParams.PageIndex,
                specParams.PageSize
            );
        }

    }
}
