using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.DTOs.Tests
{
    public class PsychologyTestOptionResponseDTO
    {
        public int Id { get; set; }
        public string DisplayedText { get; set; }
        public int Score { get; set; }
        public int PsychologyTestId { get; set; }
    }
}
