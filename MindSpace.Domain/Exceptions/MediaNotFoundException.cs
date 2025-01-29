using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Exceptions
{
    public class MediaNotFoundException : Exception
    {
        // ==================================
        // === Constructors
        // ==================================

        public MediaNotFoundException() : base("The selected media format is not supported.")
        {
        }
    }
}
