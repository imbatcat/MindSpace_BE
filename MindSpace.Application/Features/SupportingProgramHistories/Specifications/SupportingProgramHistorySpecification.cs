using MindSpace.Application.Specifications;
using MindSpace.Domain.Entities.SupportingPrograms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.SupportingProgramHistories.Specifications
{
    public class SupportingProgramHistorySpecification : BaseSpecification<SupportingProgramHistory>
    {
        public SupportingProgramHistorySpecification(SupportingProgramHistorySpecParams specParams)
            : base(h =>
                (!specParams.StudentId.HasValue || h.StudentId.Equals(specParams.StudentId)) &&
                (!specParams.SupportingProgramId.HasValue || h.SupportingProgramId.Equals(specParams.SupportingProgramId)) &&
                (!specParams.JoinedAtTo.HasValue || h.JoinedAt.Date <= specParams.JoinedAtTo) &&
                (!specParams.JoinedAtForm.HasValue || h.JoinedAt.Date >= specParams.JoinedAtForm))
        {
            // Include
            AddInclude(s => s.SupportingProgram);
            AddInclude(s => s.Student);

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
