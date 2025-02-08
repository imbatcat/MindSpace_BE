using MindSpace.Application.Specifications;

namespace MindSpace.Application.Features.Questions.Specifications
{
    public class QuestionSpecParams : BasePagingParams
    {
        public string? SearchQuestionContent { get; set; }
        public string? Sort { get; set; }
    }
}