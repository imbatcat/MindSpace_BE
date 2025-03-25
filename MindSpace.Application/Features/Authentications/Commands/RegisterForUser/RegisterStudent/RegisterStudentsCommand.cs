using MediatR;
using Microsoft.AspNetCore.Http;

namespace MindSpace.Application.Features.Authentications.Commands.RegisterForUser.RegisterStudent
{
    public class RegisterStudentsCommand : IRequest
    {
        public IFormFile file { get; set; }
    }
}