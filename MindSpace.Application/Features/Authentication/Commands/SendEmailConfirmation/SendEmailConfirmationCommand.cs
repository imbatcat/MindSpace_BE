using MediatR;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.Authentication.Commands.SendEmailConfirmation
{
    public class SendEmailConfirmationCommand(ApplicationUser user) : IRequest
    {
        public ApplicationUser User { get; } = user;
    }
}