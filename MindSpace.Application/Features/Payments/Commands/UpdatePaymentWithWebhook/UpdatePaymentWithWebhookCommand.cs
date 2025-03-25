using MediatR;
using Net.payOS.Types;

namespace MindSpace.Application.Features.Payments.Commands.UpdatePaymentWithWebhook;

public record UpdatePaymentWithWebhookCommand : IRequest
{
    public required string Code { get; set; }
    public required string Desc { get; set; }
    public required WebhookType Data { get; set; }
    public required string Signature { get; set; }
}