using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Specifications.SupportingPrograms
{
    public class SupportingProgramParams
    {
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
        public string? Sort { get; set; }
    }
}
