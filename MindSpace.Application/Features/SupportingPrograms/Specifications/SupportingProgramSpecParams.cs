using MindSpace.Application.Specifications;

namespace MindSpace.Application.Features.SupportingPrograms.Specifications
{
    public class SupportingProgramSpecParams : BasePagingParams
    {
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }

        // Sorting options (e.g., "startDateAsc", "startDateDesc", "maxQuantityAsc", "maxQuantityDesc")
        public string? Sort { get; set; }

        public DateTime? StartDateAt { get; set; }
        public int? SchoolManagerId { get; set; }
        public int? PsychologistId { get; set; }
        public int? SchoolId { get; set; }
    }
}
