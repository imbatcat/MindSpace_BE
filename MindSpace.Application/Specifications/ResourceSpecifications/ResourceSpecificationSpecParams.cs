using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Specifications.ResourceSpecifications
{
    public class ResourceSpecificationSpecParams : BasePagingParams
    {
        public string? Type { get; set; }
        public bool? IsActive { get; set; } = true;
        public int? SchoolManagerId { get; set; }
        public int? SpecializationId { get; set; }

        public string SearchTitle { get; set; }
    }
}
