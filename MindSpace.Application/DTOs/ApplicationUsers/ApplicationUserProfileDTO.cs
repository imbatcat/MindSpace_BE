namespace MindSpace.Application.DTOs.ApplicationUsers
{
    public class ApplicationUserProfileDTO
    {
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string UserName { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string ImageUrl { get; set; }
    }
}