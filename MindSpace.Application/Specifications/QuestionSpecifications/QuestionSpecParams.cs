namespace MindSpace.Application.Specifications.QuestionSpecifications
{
    public class QuestionSpecParams : BasePagingParams
    {
        public string? SearchQuestionContent { get; set; }
        public bool IsOnlyGetQuestionsWithOptions{ get; set; } = false;
        public string? Sort { get; set; }
    }
}