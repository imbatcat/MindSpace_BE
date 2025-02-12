using MediatR;

namespace MindSpace.Application.Features.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }
    }
}