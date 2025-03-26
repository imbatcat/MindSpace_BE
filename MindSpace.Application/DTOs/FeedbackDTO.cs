namespace MindSpace.Application.DTOs
{
    public class FeedbackDTO
    {
        public decimal Rating { get; set; }
        public string FeedbackContent { get; set; }
        public int StudentId { get; set; }
        public int PsychologistId { get; set; }
    }
}
