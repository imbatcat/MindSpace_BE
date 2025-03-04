namespace MindSpace.Application.DTOs.ApplicationUsers
{
    public class StudentResponseDTO : ApplicationUserResponseDTO
    {
        public SchoolDTO? School { get; set; }
    }
}