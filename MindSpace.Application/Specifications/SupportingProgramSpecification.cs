using MindSpace.Domain.Entities.SupportingPrograms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Specifications
{
    public class SupportingProgramSpecification : BaseSpecification<SupportingProgram>
    {

        public SupportingProgramSpecification(
            int? minQuantity = null,
            int? maxQuantity = null) : base(
                x =>
                (minQuantity.HasValue || x.MaxQuantity >= minQuantity) &&
                (maxQuantity.HasValue || x.MaxQuantity <= maxQuantity)
        )
        {
        }
    }
}
