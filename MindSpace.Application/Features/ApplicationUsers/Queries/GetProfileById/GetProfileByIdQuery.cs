using MediatR;
using MindSpace.Application.DTOs.ApplicationUsers;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.ViewProfileById
{
    public class GetProfileByIdQuery : IRequest<ApplicationUserProfileDTO>
    {
        public int UserId { get; set; }
    }
}
