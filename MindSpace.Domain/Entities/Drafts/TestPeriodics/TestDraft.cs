namespace MindSpace.Domain.Entities.Drafts.TestPeriodics
{
    public class TestDraft
    {
        public required string Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? TestCode { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int? TestCategoryId { get; set; } = null;
        public int? AuthorId { get; set; } = null;
        public int? SpecializationId { get; set; } = null;
        public int? QuestionCount { get; set; } = null;
        public decimal? Price { get; set; } = null;
        public List<QuestionDraft> QuestionItems { get; set; } = new List<QuestionDraft>();
    }
}
