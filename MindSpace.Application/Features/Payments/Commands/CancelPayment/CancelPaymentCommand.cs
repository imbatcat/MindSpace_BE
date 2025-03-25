using MediatR;

namespace MindSpace.Application.Features.Payments.Commands.CancelPayment;

public record CancelPaymentCommand : IRequest
{
    public int PaymentId { get; init; }
}