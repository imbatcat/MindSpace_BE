using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Exceptions
{
    public class UpdateFailedException(string resourceName) : Exception($"Update resource {resourceName} failed")
    {
    }
}
