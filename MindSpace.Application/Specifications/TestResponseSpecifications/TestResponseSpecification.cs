using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Specifications.TestResponseSpecifications
{
    public class TestResponseSpecification : BaseSpecification<TestResponse>
    {
        public TestResponseSpecification(int id)
            : base(tr => tr.Id == id)
        {
        }
        public TestResponseSpecification(TestResponseSpecParams specParams)
            : base(tr =>
                    string.IsNullOrEmpty(specParams.TestScoreRankResult) || (!string.IsNullOrEmpty(tr.TestScoreRankResult) && tr.TestScoreRankResult.ToLower().Contains(specParams.TestScoreRankResult.ToLower())) &&
                    (!specParams.StudentId.HasValue || tr.StudentId == specParams.StudentId) &&
                    (!specParams.ParentId.HasValue || tr.ParentId == specParams.ParentId) &&
                    (!specParams.TestId.HasValue || tr.TestId == specParams.TestId) &&
                    (!specParams.MaxTotalScore.HasValue || tr.TotalScore <= specParams.MaxTotalScore) &&
                    (!specParams.MinTotalScore.HasValue || tr.TotalScore >= specParams.MinTotalScore)
            )
        {
            // Add Paging
            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            // Add Sorting
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    //case "totalScore": // for psy tests only
                    //    AddOrderBy(x => x.TotalScore.ToString() ?? 0); break;
                    case "createAt":
                        AddOrderByDescending(x => x.CreateAt.ToString()); break;
                    default:
                        AddOrderBy(x => x.Id.ToString()); break;
                }
            }
        }

    }
}
