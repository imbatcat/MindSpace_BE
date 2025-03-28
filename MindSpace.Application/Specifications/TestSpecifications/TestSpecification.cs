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


        public TestSpecification(string testCode)
        : base(
            x => x.TestCode.ToLower().Equals(testCode.ToLower())
        )
        { }

        /// <summary>
        /// Constructor for General Filter and Pagination and filter if the student already complete the test
        /// </summary>
        /// <param name="specParams"></param>
        /// <param name="completedTestIds">List of test IDs that have been completed</param>
        /// <param name="excludeCompleted">If true, exclude tests that have been completed. If false, only include completed tests.</param>
        public TestSpecification(TestSpecParams specParams, List<int>? completedTestIds = null)
            : base
            (
                  t => (string.IsNullOrEmpty(specParams.Title) || t.Title.ToLower().Contains(specParams.Title.ToLower()))
                   && (string.IsNullOrEmpty(specParams.TestCode) || t.TestCode.ToLower().Contains(specParams.TestCode.ToLower()))
                   && (!specParams.TargetUser.HasValue || t.TargetUser == specParams.TargetUser)
                   && (!specParams.MinPrice.HasValue || t.Price >= specParams.MinPrice)
                   && (!specParams.MaxPrice.HasValue || t.Price <= specParams.MaxPrice)
                   && (!specParams.TestCategoryId.HasValue || t.TestCategoryId == specParams.TestCategoryId)
                   && (!specParams.SpecializationId.HasValue || t.SpecializationId == specParams.SpecializationId)
                   && (!specParams.AuthorId.HasValue || t.AuthorId == specParams.AuthorId)
                   && (!specParams.StudentId.HasValue || completedTestIds == null || !specParams.ExcludeFromCompletedTest.HasValue
                        || (specParams.ExcludeFromCompletedTest.HasValue == true ? !completedTestIds.Contains(t.Id) : completedTestIds.Contains(t.Id)))
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

        /// <summary>
        /// Constructor for 
        /// </summary>
        /// <param name="specParams"></param>
        public TestSpecification(int schoolId, int? top, DateTime? startDate, DateTime? endDate)
            : base
            (
                  t => (t.Author.SchoolManager == null || t.Author.SchoolManager.SchoolId == schoolId)
                  && (!startDate.HasValue || t.CreateAt >= startDate)
                  && (!endDate.HasValue || t.CreateAt <= endDate)
            )
        {
            AddInclude(t => t.Author);
            AddInclude(t => t.Specialization);
            if (top.HasValue) AddTop(top.Value);
            AddOrderByDescending(x => x.CreateAt.ToString());
        }
    }
}
