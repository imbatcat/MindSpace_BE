using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Tests
{
    public class PsychologyTestOption : BaseEntity
    {
        // 1 Test - M Psychology Test Options
        public int PsychologyTestId { get; set; }
        public Test PsychologyTest { get; set; }

        // Option Displayed Text
        public string DisplayedText { get; set; }

        // Option Score value
        public int Score { get; set; }
    }
}
