namespace MindSpace.Application.DTOs
{
    public class QuestionResponseDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public ICollection<QuestionOptionResponseDTO> QuestionOptions { get; set; } = new List<QuestionOptionResponseDTO>();
    }
}