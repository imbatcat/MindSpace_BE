using MediatR;
using Microsoft.AspNetCore.Http;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterStudent
{
    public class RegisterStudentsCommand : IRequest
    {
        public IFormFile file { get; set; }
    }
}