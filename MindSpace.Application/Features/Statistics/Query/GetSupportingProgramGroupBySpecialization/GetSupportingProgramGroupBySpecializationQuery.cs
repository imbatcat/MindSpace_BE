using MediatR;
using MindSpace.Application.DTOs.Statistics.SupportingProgramStatistics;

namespace MindSpace.Application.Features.Statistics.Query.GetSupportingProgramGroupBySpecialization
{
    public class GetSupportingProgramGroupBySpecializationQuery : IRequest<SupportingProgramsGroupBySpecializationDTO>
    {
        public int SchoolId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
