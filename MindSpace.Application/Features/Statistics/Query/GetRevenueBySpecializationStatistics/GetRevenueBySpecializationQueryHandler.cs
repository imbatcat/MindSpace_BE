using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Statistics.AppointmentStatistics;
using MindSpace.Application.DTOs;
using MindSpace.Application.Features.Statistics.Query.GetAppointmentGroupBySpecialization;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Application.DTOs.Statistics.SystemStatistics;
using MindSpace.Application.Specifications.InvoicesSpecifications;
using MindSpace.Domain.Entities.Constants;
using System.Linq;
using MindSpace.Application.Specifications.SpecializationSpecifications;
using MindSpace.Domain.Entities;

namespace MindSpace.Application.Features.Statistics.Query.GetRevenueBySpecializationStatistics
{
    public class GetRevenueBySpecializationQueryHandler(ILogger<GetRevenueBySpecializationQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper) : IRequestHandler<GetRevenueBySpecializationQuery, List<SpecializationRevenueStatDTO>>
    {
        public async Task<List<SpecializationRevenueStatDTO>> Handle(GetRevenueBySpecializationQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get statistics of revenue by specialization group analysis with Spec: {@Spec}", request);
            var specification = new InvoiceSpecification(request.StartDate, request.EndDate, PaymentType.Purchase, true, false, false);

            var invoices = await unitOfWork.Repository<Invoice>().GetAllWithSpecAsync(specification);
            decimal totalInvoiceAmount = invoices.Sum(invoice => invoice.Amount);

            IEnumerable<IGrouping<int, Invoice>> groupedData = invoices.GroupBy(a => a.Appointment.SpecializationId);
            List<SpecializationRevenueStatDTO> keyValuePairs = new();
            foreach (var group in groupedData)
            {
                var specialization = await unitOfWork.Repository<Specialization>().GetBySpecAsync(new SpecializationSpecification(group.Key));
                decimal revenue = group.Sum(invoice => invoice.Amount);
                keyValuePairs.Add(new SpecializationRevenueStatDTO
                {
                    Name = specialization.Name,
                    Revenue = revenue,
                    Value = revenue > 0 ? Math.Round((double)revenue / (double)totalInvoiceAmount * 100, 2) : 0
                });
            }
            return keyValuePairs;
        }
    }
}
