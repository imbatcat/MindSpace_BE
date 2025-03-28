using MindSpace.Domain.Entities;

namespace MindSpace.Application.Specifications.FeedbackSpecifications
{
    public class FeedbackSpecification : BaseSpecification<Feedback>
    {
        public FeedbackSpecification(int psychologistId) : base(x => x.PsychologistId == psychologistId)
        {
        }
    }
}
