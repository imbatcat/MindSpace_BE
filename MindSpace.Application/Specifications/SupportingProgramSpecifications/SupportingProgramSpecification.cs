using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.Application.Specifications.SupportingProgramSpecifications
{
    public class SupportingProgramSpecification : BaseSpecification<SupportingProgram>
    {
        // =====================================
        // === Constructors
        // =====================================


        /// <summary>
        /// Get all supporting programs
        /// </summary>
        public SupportingProgramSpecification() : base(x => true)
        {
        }

        /// <summary>
        /// Filter by Program Id
        /// </summary>
        /// <param name="programId"></param>
        public SupportingProgramSpecification(int programId)
            : base(x => x.Id.Equals(programId))
        {
            AddInclude(x => x.Psychologist);
        }

        /// <summary>
        /// Filter by Title for Create
        /// </summary>
        /// <param name="title"></param>
        public SupportingProgramSpecification(string title)
            : base(x => x.Title.ToLower().Contains(title.ToLower()))
        {
        }

        /// <summary>
        /// Using short circuit
        /// if FALSE || TRUE, then consider the TRUE expression
        /// if TRUE || ..., if left side is TRUE, which mean don't evaluate value of right side
        /// 
        /// if all NULLs => all TRUES => Get ALL values from the table
        /// 
        /// Constructor for General Filter and Pagination
        /// </summary>
        /// <param name="specParams"></param>
        public SupportingProgramSpecification(SupportingProgramSpecParams specParams)
            : base(x =>
                (string.IsNullOrEmpty(specParams.SearchTitle) || x.Title.ToLower().Contains(specParams.SearchTitle!.ToLower())) &&
                (!specParams.MinQuantity.HasValue || x.MaxQuantity >= specParams.MinQuantity) &&
                (!specParams.MaxQuantity.HasValue || x.MaxQuantity <= specParams.MaxQuantity) &&
                (!specParams.SchoolManagerId.HasValue || x.SchoolManagerId.Equals(specParams.SchoolManagerId)) &&
                (!specParams.SchoolId.HasValue || x.SchoolId.Equals(specParams.SchoolId)) &&
                (!specParams.PsychologistId.HasValue || x.PsychologistId.Equals(specParams.PsychologistId)) &&
                (!specParams.StartDateAt.HasValue || x.StartDateAt.Equals(specParams.StartDateAt)) &&
                (!specParams.IsActive.HasValue || x.IsActive == specParams.IsActive))
        {
            // Add Paging
            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            // Add Sorting
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "maxQuantityAsc":
                        AddOrderBy(x => x.MaxQuantity.ToString()); break;
                    case "maxQuantityDesc":
                        AddOrderByDescending(x => x.MaxQuantity.ToString()); break;
                    case "startDateAsc":
                        AddOrderBy(x => x.StartDateAt); break;
                    case "startDateDesc":
                        AddOrderByDescending(x => x.StartDateAt); break;
                    default:
                        AddOrderBy(x => x.Id.ToString()); break;
                }
            }
        }

        public SupportingProgramSpecification(int schoolId, DateTime? startDate, DateTime? endDate) : base(
            a => a.SchoolId == schoolId
            && (!startDate.HasValue || a.CreateAt >= startDate)
            && (!endDate.HasValue || a.CreateAt <= endDate)
            )
        {
            AddInclude(a => a.Psychologist);
        }
    }
}