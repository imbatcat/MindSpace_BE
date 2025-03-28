using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Statistics.SystemStatistics;
using MindSpace.Application.DTOs.Statistics.TestResponseStatistics;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.InvoicesSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Features.Statistics.Query.GetRevenueTimeAnalysisStatistics
{
    public class GetRevenueTimeAnalysisStatisticsHandler(ILogger<GetRevenueTimeAnalysisStatisticsHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper) : IRequestHandler<GetRevenueTimeAnalysisStatisticsQuery, List<RevenueStatDTO>>
    {
        public async Task<List<RevenueStatDTO>> Handle(GetRevenueTimeAnalysisStatisticsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get statistics of invoices by time group analysis with Spec: {@Spec}", request);
            var specification = new InvoiceSpecification(request.StartDate, request.EndDate, null, true, true, false);

            // Get the filtered list of TestResponse objects
            var invoices = await unitOfWork.Repository<Invoice>().GetAllWithSpecAsync(specification);

            // Group the data in-memory based on TimePeriod
            IEnumerable<IGrouping<string, Invoice>> groupedData;
            switch (request.GroupBy.ToLower())
            {
                case "date":
                    groupedData = invoices.GroupBy(x => x.CreateAt.ToString("yyyy-MM-dd")); break;
                case "month":
                    groupedData = invoices.GroupBy(x => x.CreateAt.ToString("yyyy-MM")); break;
                default:
                    groupedData = invoices.GroupBy(x => x.CreateAt.ToString("yyyy-MM-dd")); break;
            }

            // Transform the grouped data into TimeGroupDto objects
            var timeGroups = new List<RevenueStatDTO>();
            foreach (var group in groupedData)
            {
                timeGroups.Add(RevenueStatDTO.MapToDTO(group, request.GroupBy));
            }

            return timeGroups;
        }

    }
}
