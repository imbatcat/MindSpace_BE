using MediatR;
using MindSpace.Application.DTOs;

namespace MindSpace.Application.Features.Authentication.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<LoginResponseDTO>
    {
        public required string Email { get; set; } = default!;
        public required string Password { get; set; } = default!;
    }
}
