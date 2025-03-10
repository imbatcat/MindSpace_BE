using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Specifications.TestSpecifications
{
    public class TestSpecification : BaseSpecification<Test>
    {
        /// <summary>
        /// Filter by Test Id
        /// </summary>
        /// <param name="testId"></param>
        public TestSpecification(int testId)
            : base(x => x.Id.Equals(testId))
        {
        }


        public TestSpecification(string title, int authorId, string testCode)
        : base(
            x =>
                (x.Title.ToLower().Equals(title.ToLower()) || x.TestCode.ToLower().Equals(testCode.ToLower()))
                && x.AuthorId == authorId
            )
        {
        }


        /// <summary>
        /// Constructor for General Filter and Pagination
        /// </summary>
        /// <param name="specParams"></param>
        public TestSpecification(TestSpecParams specParams)
            : base
            (
                  t =>
                  (
                   string.IsNullOrEmpty(specParams.Title) || t.Title.ToLower().Contains(specParams.Title.ToLower()))
                   && (
                        string.IsNullOrEmpty(specParams.TestCode)
                        || (
                            !string.IsNullOrEmpty(t.TestCode)
                             && t.TestCode.ToLower().Contains(specParams.TestCode.ToLower())
                           )
                      )
                   && (!specParams.TargetUser.HasValue || t.TargetUser == specParams.TargetUser)
                   && (!specParams.MinPrice.HasValue || t.Price >= specParams.MinPrice)
                   && (!specParams.MaxPrice.HasValue || t.Price <= specParams.MaxPrice)
                   && (!specParams.TestCategoryId.HasValue || t.TestCategoryId == specParams.TestCategoryId)
                   && (!specParams.SpecializationId.HasValue || t.SpecializationId == specParams.SpecializationId)
                   && (!specParams.AuthorId.HasValue || t.AuthorId == specParams.AuthorId)
            )
        {

            // Add Paging
            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            // Add Sorting
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "title":
                        AddOrderBy(x => x.Title); break;
                    case "questionCount":
                        AddOrderBy(x => x.QuestionCount); break;
                    case "createAt":
                        AddOrderByDescending(x => x.CreateAt.ToString()); break;
                    default:
                        AddOrderBy(x => x.Id.ToString()); break;
                }
            }
        }
    }
}
