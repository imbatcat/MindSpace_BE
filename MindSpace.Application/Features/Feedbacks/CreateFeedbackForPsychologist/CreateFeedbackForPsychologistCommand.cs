using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.Feedbacks.CreateFeedbackForPsychologist
{
    public class CreateFeedbackForPsychologistCommand : IRequest
    {
        public int PsychologistId { get; set; }
        public int StudentId { get; set; }
        public decimal Rating { get; set; }
        public string FeedbackContent { get; set; }
    }
}
