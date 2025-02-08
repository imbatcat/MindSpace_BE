using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Interfaces.Services.Authentication;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterParent
{
    internal class RegisterParentCommandHandler
            (ILogger<RegisterParentCommandHandler> logger,
            IApplicationUserService applicationUserService) : IRequestHandler<RegisterParentCommand>
    {
        public async Task Handle(RegisterParentCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser parent = new ApplicationUser()
            {
                Email = request.Email,
                UserName = request.Username,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = DateTime.Parse(request.Birthdate),
            };
            await applicationUserService.InsertAsync(parent, request.Password);
        }
    }
}