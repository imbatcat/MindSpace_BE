using MediatR;
using Microsoft.AspNetCore.Http;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterManager
{
    public class RegisterSchoolManagerCommand : IRequest
    {
        public IFormFile file { get; set; }
    }
}