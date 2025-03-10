using MediatR;

namespace MindSpace.Application.Features.Authentications.Commands.RevokeUser
{
    public class RevokeUserCommand : IRequest
    {
        public string UserId { get; set; }
    }
}