using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.DTOs.Invoices;
using MindSpace.Application.Specifications.InvoicesSpecifications;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.Invoices.Queries.GetInvoiceList
{
    public class GetInvoiceListQueryHandler(
    IUnitOfWork _unitOfWork,
    ILogger<GetInvoiceListQueryHandler> _logger,
    IMapper _mapper) : IRequestHandler<GetInvoiceListQuery, PagedResultDTO<InvoiceDTO>>
    {
        public async Task<PagedResultDTO<InvoiceDTO>> Handle(GetInvoiceListQuery request, CancellationToken cancellationToken)
        {
            var specification = new InvoiceSpecification(request.SpecParams);
            try
            {
                var invoices = await _unitOfWork.Repository<Invoice>().GetAllWithSpecAsync(specification);
                var invoiceDTOs = _mapper.Map<List<InvoiceDTO>>(invoices);
                var count = await _unitOfWork.Repository<Invoice>().CountAsync(specification);
                _logger.LogInformation("Found {Count} invoices", invoiceDTOs.Count);
                return new PagedResultDTO<InvoiceDTO>(count, invoiceDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting payments");
                throw;
            }
        }
    }
}
