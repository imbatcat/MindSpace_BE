using MediatR;
using MindSpace.Application.Features.Authentication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.Authentication.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<LoginResponseDTO>
    {
        public required string Email { get; set; } = default!;
        public required string Password { get; set; } = default!;
    }
}
