namespace MindSpace.Application.Specifications.QuestionSpecifications
{
    public class QuestionSpecParams : BasePagingParams
    {
        public string? SearchQuestionContent { get; set; }
        public string? Sort { get; set; }
    }
}