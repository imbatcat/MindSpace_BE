using MindSpace.Application.Specifications;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.Application.Features.SupportingPrograms.Specifications
{
    public class SupportingProgramSpecification : BaseSpecification<SupportingProgram>
    {
        // =====================================
        // === Constructors
        // =====================================

        /// <summary>
        /// Using short circuit
        /// if FALSE || TRUE, then consider the TRUE
        /// if TRUE || ..., then first one always TRUE, which maesn don't have value of MinQuantity and MaxQuantity
        /// 
        /// Constructor for Pagination and Query
        /// </summary>
        /// <param name="specParams"></param>
        public SupportingProgramSpecification(SupportingProgramSpecParams specParams)
            : base(x =>
                (!specParams.MinQuantity.HasValue || x.MaxQuantity >= specParams.MinQuantity) &&
                (!specParams.MaxQuantity.HasValue || x.MaxQuantity <= specParams.MaxQuantity) &&
                (!specParams.SchoolManagerId.HasValue || x.SchoolManagerId.Equals(specParams.SchoolManagerId)) &&
                (!specParams.SchoolId.HasValue || x.SchoolId.Equals(specParams.SchoolId)) &&
                (!specParams.PsychologistId.HasValue || x.PsychologistId.Equals(specParams.PsychologistId)) &&
                (!specParams.StartDateAt.HasValue || x.StartDateAt.Equals(specParams.StartDateAt)))
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
    }
}
