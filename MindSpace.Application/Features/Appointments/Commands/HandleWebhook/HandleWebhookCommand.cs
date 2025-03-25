using MediatR;

namespace MindSpace.Application.Features.Appointments.Commands.HandleWebhook
{
    public class HandleWebhookCommand : IRequest
    {
        public required string StripeEventJson { get; set; }
    }
}
