using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.Application.Specifications.SupportingProgramHistorySpecifications
{
    public class SupportingProgramHistorySpecification : BaseSpecification<SupportingProgramHistory>
    {
        /// <summary>
        /// Filter by Student Id
        /// </summary>
        /// <param name="studentId"></param>
        public SupportingProgramHistorySpecification(int studentId)
            : base(h => (h.StudentId.Equals(studentId)))
        {
            AddOrderByDescending(x => x.JoinedAt);
        }

        /// <summary>
        /// Filter by Student Id and Supporting Program Id
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="supportingProgramId"></param>
        public SupportingProgramHistorySpecification(int studentId, int supportingProgramId)
            : base(h => (h.StudentId == studentId && h.SupportingProgramId == supportingProgramId))
        {
        }

        /// <summary>
        /// General Filter
        /// </summary>
        /// <param name="specParams"></param>
        public SupportingProgramHistorySpecification(SupportingProgramHistorySpecParams specParams)
            : base(h =>
                (!specParams.StudentId.HasValue || h.StudentId.Equals(specParams.StudentId)) &&
                (!specParams.SupportingProgramId.HasValue || h.SupportingProgramId.Equals(specParams.SupportingProgramId)) &&
                (!specParams.JoinedAtTo.HasValue || h.JoinedAt.Date <= specParams.JoinedAtTo) &&
                (!specParams.JoinedAtForm.HasValue || h.JoinedAt.Date >= specParams.JoinedAtForm))
        {
            // Paging
            AddPaging(
                specParams.PageSize * (specParams.PageIndex - 1),
                specParams.PageSize);

            // Sort
            switch (specParams.Sort)
            {
                case "joinedAtAsc":
                    AddOrderBy(h => h.StudentId);
                    break;

                case "joinedAtDesc":
                    AddOrderByDescending(h => h.SupportingProgramId);
                    break;

                default:
                    AddOrderBy(h => h.StudentId);
                    break;
            }
        }


    }
}