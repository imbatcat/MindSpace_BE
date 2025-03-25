using MediatR;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.Authentications.Commands.SendEmailConfirmation
{
    public class SendEmailConfirmationCommand(ApplicationUser user) : IRequest
    {
        public ApplicationUser User { get; } = user;
    }
}