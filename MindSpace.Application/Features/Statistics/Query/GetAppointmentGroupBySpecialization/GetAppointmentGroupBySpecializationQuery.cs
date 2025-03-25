using MediatR;
using MindSpace.Application.DTOs.Statistics.AppointmentStatistics;

namespace MindSpace.Application.Features.Statistics.Query.GetAppointmentGroupBySpecialization
{
    public class GetAppointmentGroupBySpecializationQuery : IRequest<AppointmentGroupBySpecializationDTO>
    {
        public int SchoolId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
