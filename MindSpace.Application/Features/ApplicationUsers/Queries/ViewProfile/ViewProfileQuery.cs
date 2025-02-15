using MediatR;
using MindSpace.Application.DTOs.ApplicationUsers;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.ViewProfile
{
    public class ViewProfileQuery : IRequest<ApplicationUserProfileDTO>
    {
    }
}