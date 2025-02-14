using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Drafts.TestPeriodics
{
    public class QuestionDraft
    {
        public required int Id { get; set; }
        public string Content { get; set; }
        public List<OptionDraft> QuestionOptions { get; set; } = new List<OptionDraft>();
    }
}
