using MindSpace.Application.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.Questions.Specifications
{
    public class QuestionSpecParams : BasePagingParams
    {
        public string? SearchQuestionContent { get; set; }
        public string? Sort { get; set; }
    }
}
