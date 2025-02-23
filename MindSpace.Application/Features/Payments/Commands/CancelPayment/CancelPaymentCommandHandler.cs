using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Specifications.PaymentSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Payments.Commands.CancelPayment;

public class CancelPaymentCommandHandler(
    ILogger<CancelPaymentCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IPaymentService paymentService) : IRequestHandler<CancelPaymentCommand>
{
    public async Task Handle(CancelPaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await CancelPaymentLinkAsync(request.PaymentId);
            await UpdatePaymentStatusAsync(request.PaymentId);

            logger.LogInformation("Payment {PaymentId} cancelled successfully", request.PaymentId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error cancelling payment {PaymentId}", request.PaymentId);
            throw;
        }
    }

    private async Task CancelPaymentLinkAsync(int paymentId)
    {
        await paymentService.CancelPaymentLinkAsync(paymentId);
    }

    private async Task UpdatePaymentStatusAsync(int paymentId)
    {
        var specification = new PaymentSpecification(paymentId);
        var payment = (await unitOfWork.Repository<Payment>().GetAllWithSpecAsync(specification))
            .FirstOrDefault()
            ?? throw new NotFoundException(nameof(Payment), paymentId.ToString());

        payment.Status = PaymentStatus.Failed;
        payment.UpdateAt = DateTime.UtcNow;

        unitOfWork.Repository<Payment>().Update(payment);
        await unitOfWork.CompleteAsync();
    }
}
