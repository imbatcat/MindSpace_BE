using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Specifications.QuestionSpecifications
{
    public class QuestionSpecification : BaseSpecification<Question>
    {
        /// <summary>
        /// Filter by Question Id
        /// </summary>
        /// <param name="questionId"></param>
        public QuestionSpecification(int questionId)
            : base(x => x.Id.Equals(questionId))
        {
        }

        /// <summary>
        /// 
        /// Constructor for General Filter and Pagination
        /// </summary>
        /// <param name="specParams"></param>
        public QuestionSpecification(QuestionSpecParams specParams)
            : base(q => string.IsNullOrEmpty(specParams.SearchQuestionContent) || q.Content.ToLower().Contains(specParams.SearchQuestionContent.ToLower()))
        {

            // Add Paging
            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            // Add Sorting
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "content":
                        AddOrderBy(x => x.Content); break;
                    case "createAt":
                        AddOrderByDescending(x => x.CreateAt.ToString()); break;
                    default:
                        AddOrderBy(x => x.Id.ToString()); break;
                }
            }
        }
    }
}