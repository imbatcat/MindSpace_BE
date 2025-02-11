using MediatR;
using Microsoft.AspNetCore.Http;

namespace MindSpace.Application.Features.Authentication.Commands.LogoutUser
{
    public class LogoutUserCommand : IRequest
    {
        public HttpResponse Response { get; set; }
    }
}