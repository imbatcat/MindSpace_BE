namespace MindSpace.Application.DTOs
{
    public class SupportingProgramDTO
    {
        public int Id { get; set; }
        public string ThumbnailUrl { get; set; }
        public string PdffileUrl { get; set; }
        public int MaxQuantity { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Ward { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
        public DateTime StartDateAt { get; set; }

        //public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
