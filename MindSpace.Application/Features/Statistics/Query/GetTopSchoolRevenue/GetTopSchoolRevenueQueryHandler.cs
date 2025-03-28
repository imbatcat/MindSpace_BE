using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Statistics.SystemStatistics;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.InvoicesSpecifications;
using MindSpace.Application.Specifications.SchoolSpecifications;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.Features.Statistics.Query.GetTopSchoolRevenue
{
    public class GetTopSchoolRevenueQueryHandler(ILogger<GetTopSchoolRevenueQueryHandler> logger,
        IUnitOfWork unitOfWork) : IRequestHandler<GetTopSchoolRevenueQuery, List<SchoolRevenueDTO>>
    {
        public async Task<List<SchoolRevenueDTO>> Handle(GetTopSchoolRevenueQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get statistics of top school with highest revenue with Spec: {@Spec}", request);
            var specification = new InvoiceSpecification(request.StartDate, request.EndDate, PaymentType.Purchase, false, false, true);

            var invoices = await unitOfWork.Repository<Invoice>().GetAllWithSpecAsync(specification);

            IEnumerable<IGrouping<int, Invoice>> groupedData = invoices.GroupBy(a => a.Account.Student.SchoolId);
            List<SchoolRevenueDTO> keyValuePairs = new();
            foreach (var group in groupedData)
            {
                decimal revenue = group.Sum(invoice => invoice.Amount);
                var school = await unitOfWork.Repository<School>().GetBySpecAsync(new SchoolSpecifications(group.Key, true));
                keyValuePairs.Add(new SchoolRevenueDTO
                {
                    Name = school.SchoolName,
                    Revenue = revenue
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
