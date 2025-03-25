using MediatR;
using MindSpace.Application.DTOs.ApplicationUsers;

namespace MindSpace.Application.Features.ApplicationUsers.Commands.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<ApplicationUserProfileDTO>
    {
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string ImgUrl { get; set; }
        public string? Bio { get; set; }
        public float? AverageRating { get; set; }
        public decimal? SessionPrice { get; set; }
        public decimal? ComissionRate { get; set; }
    }
}