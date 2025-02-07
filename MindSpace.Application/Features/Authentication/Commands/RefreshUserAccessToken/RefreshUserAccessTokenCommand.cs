using MediatR;
using MindSpace.Application.Features.Authentication.DTOs;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.Authentication.Commands.RefreshUserAccessToken
{
    public class RefreshUserAccessTokenCommand(ApplicationUser user) : IRequest<RefreshUserAccessTokenDTO>
    {
        public ApplicationUser User { get; set; } = user;
    }
}