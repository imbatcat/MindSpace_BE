using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.Specifications.TestSpecifications
{
    public class TestSpecParams : BasePagingParams
    {
        public string? Title { get; set; }
        public string? TestCode { get; set; }
        public TargetUser? TargetUser { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? TestCategoryId { get; set; }
        public int? SpecializationId { get; set; }
        public string? Sort { get; set; }
    }
}
