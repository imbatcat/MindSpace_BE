namespace MindSpace.Application.DTOs
{
    public class StudentResponseDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string ImgUrl { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}