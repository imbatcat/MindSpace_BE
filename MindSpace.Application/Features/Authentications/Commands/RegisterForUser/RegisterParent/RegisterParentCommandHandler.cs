using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.Authentications.Commands.RegisterForUser.RegisterParent
{
    internal class RegisterParentCommandHandler
            (ILogger<RegisterParentCommandHandler> logger,
            IApplicationUserService<ApplicationUser> applicationUserService) : IRequestHandler<RegisterParentCommand>
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
            await applicationUserService.AssignRoleAsync(parent, UserRoles.Parent);
        }
    }
}