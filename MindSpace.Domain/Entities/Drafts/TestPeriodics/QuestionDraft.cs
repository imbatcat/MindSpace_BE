using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Drafts.TestPeriodics
{
    public class QuestionDraft
    {
        public required int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public List<OptionDraft> Options { get; set; } = new List<OptionDraft>();
    }
}
