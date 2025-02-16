using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.Specifications.ResourceSpecifications
{
    public class ResourceSpecificationSpecParams : BasePagingParams
    {
        public string? Type { get; set; }
        public bool? IsActive { get; set; }
        public int? SchoolManagerId { get; set; }
        public int? SpecializationId { get; set; }
        public string? SearchTitle { get; set; }
    }
}
