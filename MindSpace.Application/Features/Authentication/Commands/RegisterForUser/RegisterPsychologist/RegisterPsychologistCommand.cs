using MediatR;
using Microsoft.AspNetCore.Http;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterPsychologist
{
    public class RegisterPsychologistCommand : IRequest
    {
        public IFormFile file { get; set; }
    }
}