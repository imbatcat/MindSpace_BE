using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Invoices;
using MindSpace.Application.Specifications.InvoicesSpecifications;

namespace MindSpace.Application.Features.Invoices.Queries.GetInvoiceHistoryByUser
{
    public class GetInvoiceHistoryByUserQuery : IRequest<PagedResultDTO<InvoiceDTO>>
    {
        public InvoiceSpecParams SpecParams { get; set; }
        public GetInvoiceHistoryByUserQuery(InvoiceSpecParams specParams)
        {
            SpecParams = specParams;
        }
    }
}
