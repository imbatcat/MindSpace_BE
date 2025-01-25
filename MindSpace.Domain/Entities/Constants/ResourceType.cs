using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Constants
{
    internal enum ResourceType
    {
        [EnumMember(Value = "Blog")]
        Blog,

        [EnumMember(Value = "Article")]
        Article,

        [EnumMember(Value = "File")]
        File,
    }
}
