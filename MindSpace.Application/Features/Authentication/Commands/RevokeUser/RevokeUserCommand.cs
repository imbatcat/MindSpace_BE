using MediatR;

namespace MindSpace.Application.Features.Authentication.Commands.RevokeUser
{
    public class RevokeUserCommand : IRequest
    {
        public string UserId { get; set; }
    }
}