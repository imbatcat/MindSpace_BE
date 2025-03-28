using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Statistics.SystemStatistics;
using MindSpace.Application.Features.Statistics.Query.GetTestResponseTimeAnalysisStatistics;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.InvoicesSpecifications;
using MindSpace.Application.Specifications.SpecializationSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.Statistics.Query.GetTopPsychologistRevenueStatistics
{
    public class GetTopPsychologistRevenueQueryHandler(ILogger<GetTestResponseTimeAnalysisStatisticsQueryHandler> logger,
            IUnitOfWork unitOfWork) : IRequestHandler<GetTopPsychologistRevenueQuery, List<PsychologistRevenueStatDTO>>
    {
        public async Task<List<PsychologistRevenueStatDTO>> Handle(GetTopPsychologistRevenueQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get statistics of top psychologists with highest revenue with Spec: {@Spec}", request);
            var specification = new InvoiceSpecification(request.StartDate, request.EndDate, PaymentType.Purchase, true, true, false);

            var invoices = await unitOfWork.Repository<Invoice>().GetAllWithSpecAsync(specification);

            IEnumerable<IGrouping<Psychologist, Invoice>> groupedData = invoices.GroupBy(a => a.Appointment.Psychologist);
            List<PsychologistRevenueStatDTO> keyValuePairs = new();
            foreach (var group in groupedData)
            {
                decimal revenue = group.Sum(invoice => invoice.Amount);
                decimal systemProfit = revenue * (1 - group.Key.ComissionRate);
                keyValuePairs.Add(new PsychologistRevenueStatDTO
                {
                    Name = group.Key.FullName,
                    Revenue = revenue,
                    SystemProfit = systemProfit
                });
            }
            
            var topKeyValuePairs = keyValuePairs
                .OrderByDescending(kvp => kvp.Revenue) 
                .Take(request.Top) 
                .ToList();
            return keyValuePairs;
        }
    }


}
