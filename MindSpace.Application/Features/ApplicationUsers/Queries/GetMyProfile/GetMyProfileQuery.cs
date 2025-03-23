using MediatR;
using MindSpace.Application.DTOs.ApplicationUsers;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.GetMyProfile
{
    public class GetMyProfileQuery : IRequest<ApplicationUserProfileDTO>
    {
    }
}