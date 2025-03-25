using MediatR;

namespace MindSpace.Application.Features.Authentications.Commands.RegisterForUser.RegisterParent
{
    public class RegisterParentCommand : IRequest
    {
        public string Email { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Birthdate { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}