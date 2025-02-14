namespace MindSpace.Domain.Entities.Drafts.TestPeriodics
{
    public class TestDraft
    {
        public required string Id { get; set; }
        public string Title { get; set; }
        public string TestCode { get; set; }
        public string Description { get; set; }
        public int TestCategoryId { get; set; }
        public int AuthorId { get; set; }
        public int SpecializationId { get; set; }
        public int QuestionCount { get; set; }
        public decimal Price { get; set; }
        public List<QuestionDraft> QuestionItems { get; set; } = new List<QuestionDraft>();
    }
}
