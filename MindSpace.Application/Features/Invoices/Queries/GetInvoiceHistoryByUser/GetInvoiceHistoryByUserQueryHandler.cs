using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Invoices;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using MindSpace.Application.Specifications.InvoicesSpecifications;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.Invoices.Queries.GetInvoiceHistoryByUser
{
    public class GetInvoiceHistoryByUserQueryHandler(
    IUnitOfWork _unitOfWork,
    ILogger<GetInvoiceHistoryByUserQueryHandler> _logger,
    IMapper _mapper,
    IUserContext _userContext
) : IRequestHandler<GetInvoiceHistoryByUserQuery, PagedResultDTO<InvoiceDTO>>
    {
        public async Task<PagedResultDTO<InvoiceDTO>> Handle(GetInvoiceHistoryByUserQuery request, CancellationToken cancellationToken)
        {
            var userClaims = _userContext.GetCurrentUser();
            _logger.LogInformation("Getting appointments for user {UserEmail}", userClaims!.Email);
            var specification = new InvoiceSpecification(request.SpecParams, userClaims!.Email);
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
                _logger.LogError(ex, "Error getting invoices for user {UserEmail}", userClaims!.Email);
                throw;
            }
        }
    }

}
