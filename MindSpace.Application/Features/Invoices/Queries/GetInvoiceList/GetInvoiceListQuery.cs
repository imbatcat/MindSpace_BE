using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Invoices;
using MindSpace.Application.Specifications.InvoicesSpecifications;

namespace MindSpace.Application.Features.Invoices.Queries.GetInvoiceList
{
    public class GetInvoiceListQuery : IRequest<PagedResultDTO<InvoiceDTO>>
    {
        public InvoiceSpecParams SpecParams { get; set; }
        public GetInvoiceListQuery(InvoiceSpecParams specParams) {
            SpecParams = specParams;
        }
    }
}
