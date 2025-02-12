using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Drafts.Blogs
{
    public class SectionDraft
    {
        public required string Id { get; set; }
        public string Heading { get; set; }
        public string HtmlContent { get; set; }
    }
}
