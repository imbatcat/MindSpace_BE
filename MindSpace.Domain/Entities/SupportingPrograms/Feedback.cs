using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Entities.SupportingPrograms
{
    public class Feedback : BaseEntity
    {
        public decimal Rating { get; set; }
        public string FeedbackContent { get; set; }


        // 1 Psychologist - M Feedbacks
        public int PsychologistId { get; set; }
        public Psychologist Psychologist { get; set; }


        // 1 Student - M Feedbacks
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
