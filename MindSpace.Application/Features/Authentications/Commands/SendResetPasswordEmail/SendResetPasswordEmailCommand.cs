using MediatR;

namespace MindSpace.Application.Features.Authentications.Commands.SendResetPasswordEmail
{
    public class SendResetPasswordEmailCommand : IRequest
    {
        public string Email { get; set; }
    }
}