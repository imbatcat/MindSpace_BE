

namespace MindSpace.Application.DTOs.ApplicationUsers
{
    public class ApplicationUserResponseDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
    }
}