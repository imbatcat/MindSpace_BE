using MediatR;

namespace MindSpace.Application.Features.Authentication.Commands.SendResetPasswordEmail
{
    public class SendResetPasswordEmailCommand : IRequest
    {
        public string Email { get; set; }
    }
}