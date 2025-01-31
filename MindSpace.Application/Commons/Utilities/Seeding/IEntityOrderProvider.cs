using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Commons.Utilities.Seeding
{
    public interface IEntityOrderProvider
    {
        IEnumerable<string> GetOrderedEntities();
    }
}
