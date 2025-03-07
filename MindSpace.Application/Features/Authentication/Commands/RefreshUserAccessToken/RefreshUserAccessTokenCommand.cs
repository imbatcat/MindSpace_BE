using MediatR;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.Authentication.Commands.RefreshUserAccessToken
{
    public class RefreshUserAccessTokenCommand(ApplicationUser user) : IRequest<RefreshUserAccessTokenDTO>
    {
        public ApplicationUser User { get; set; } = user;
    }
}