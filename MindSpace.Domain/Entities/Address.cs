using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities
{
    public class Address
    {
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Ward { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
    }
}
