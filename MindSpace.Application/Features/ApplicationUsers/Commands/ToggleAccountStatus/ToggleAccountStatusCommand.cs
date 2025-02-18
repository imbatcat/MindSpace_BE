using MediatR;

namespace MindSpace.Application.Features.ApplicationUsers.Commands.ToggleAccountStatus
{
    public class ToggleAccountStatusCommand : IRequest
    {
        public int UserId { get; set; }
    }
}