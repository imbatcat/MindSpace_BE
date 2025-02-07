using MediatR;
using MindSpace.Application.Features.Authentication.Services;
using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterForUser.RegisterParent
{
    internal class RegisterParentCommandHandler
        (UserRegistrationService userRegistrationService) : IRequestHandler<RegisterParentCommand>
    {
        public async Task Handle(RegisterParentCommand request, CancellationToken cancellationToken)
        {
            var dto = new RegisterUserDTO()
            {
                Email = request.Email,
                Password = request.Password,
                UserName = request.Email.ToLower(),
                Role = UserRoles.Parent
            };
            await userRegistrationService.RegisterUserAsync(dto);
        }
    }
}