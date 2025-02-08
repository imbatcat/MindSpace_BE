using MindSpace.Application.Specifications;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Features.QuestionOptions.Specifications
{
    public class QuestionOptionSpecification : BaseSpecification<QuestionOption>
    {
        public QuestionOptionSpecification(QuestionOptionSpecParams specParams)
            : base(qo =>
                (string.IsNullOrEmpty(specParams.SearchOptionDisplayedText) || qo.DisplayedText.Contains(specParams.SearchOptionDisplayedText)) &&
                (!specParams.QuestionId.HasValue || qo.QuestionId == specParams.QuestionId)
            )
        {
            AddInclude(qo => qo.Question);
        }
    }
}