using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.DTOs
{
    public class QuestionOptionResponseDTO
    {
        public int Id { get; set; }
        public string DisplayedText { get; set; }
        public int QuestionId { get; set; }
    }
}
