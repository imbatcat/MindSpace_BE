using MediatR;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterParent
{
    public class RegisterParentCommand : IRequest
    {
        public required string Email { get; set; } = default!;
        public required string Username { get; set; } = default!;
        public required string Password { get; set; } = default!;
        public required string Birthdate { get; set; } = default!;
        public required string PhoneNumber { get; set; } = default!;
    }
}
