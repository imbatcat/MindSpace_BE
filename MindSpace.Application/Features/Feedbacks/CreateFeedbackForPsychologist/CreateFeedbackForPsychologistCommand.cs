using MediatR;

namespace MindSpace.Application.Features.Feedbacks.CreateFeedbackForPsychologist
{
    public class CreateFeedbackForPsychologistCommand : IRequest
    {
        public string RoomId { get; set; }
        public decimal Rating { get; set; }
        public string FeedbackContent { get; set; }
    }
}
