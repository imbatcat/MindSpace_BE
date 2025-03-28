using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.Specifications.AppointmentSpecifications
{
    public class AppointmentSpecParams : BasePagingParams
    {
        public string? Sort { get; set; } = "dateAsc";
        public string? PsychologistName { get; set; }
        public string? StudentEmail { get; set; }
        public DateOnly StartDate { get; set; } = new DateOnly(1, 1, 1);
        public DateOnly EndDate { get; set; } = new DateOnly(9999, 12, 31);
        public int? SchoolId { get; set; }
        public AppointmentStatus? AppointmentStatus { get; set; }
    }
}