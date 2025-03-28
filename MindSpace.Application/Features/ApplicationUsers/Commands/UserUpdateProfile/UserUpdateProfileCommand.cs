using MediatR;
using Microsoft.AspNetCore.Http;
using MindSpace.Application.DTOs.ApplicationUsers;

namespace MindSpace.Application.Features.ApplicationUsers.Commands.UserUpdateProfile
{
    public class UserUpdateProfileCommand : IRequest<ApplicationUserProfileDTO>
    {
        public required string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public IFormFile? Image { get; set; }
        public string? Bio { get; set; }
        public decimal? SessionPrice { get; set; }
    }
}