using MediatR;
using Microsoft.AspNetCore.Http;

namespace MindSpace.Application.Features.Authentications.Commands.RegisterForUser.RegisterPsychologist
{
    public class RegisterPsychologistCommand : IRequest
    {
        public IFormFile file { get; set; }
    }
}