using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Exceptions
{
    public class ForbiddenException : Exception
    {
        // ==================================
        // === Constructors
        // ==================================

        public ForbiddenException()
            : base("You do not have permission to access this resource.")
        {
        }
    }
}
