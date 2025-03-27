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
        public string RoomId { get; set; }
        public decimal Rating { get; set; }
        public string FeedbackContent { get; set; }
    }
}
