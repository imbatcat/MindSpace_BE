using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services;
using MindSpace.Application.Specifications.PaymentSpecifications;
using AutoMapper;

namespace MindSpace.Application.Features.Payments.Commands.UpdatePaymentWithWebhook;

public class UpdatePaymentWithWebhookCommandHandler(
    ILogger<UpdatePaymentWithWebhookCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IPaymentService paymentService
    ) : IRequestHandler<UpdatePaymentWithWebhookCommand>
{
    public async Task Handle(UpdatePaymentWithWebhookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Processing payment webhook: {WebhookPayload}", request.Data.data.accountNumber);

            var verifiedData = await paymentService.VerifyWebhookDataAsync(request.Data);

            var specification = new PaymentSpecification(int.Parse(verifiedData.OrderCode));
            var payment = await unitOfWork.Repository<Payment>().GetBySpecAsync(specification);

            if (payment == null)
            {
                logger.LogError("Payment not found for transaction code: {TransactionCode}", verifiedData.OrderCode);
                throw new NotFoundException(nameof(Payment), verifiedData.OrderCode);
            }

            await paymentService.UpdatePaymentFromWebhookAsync(payment, verifiedData);
            await unitOfWork.CompleteAsync();

            logger.LogInformation("Successfully processed payment webhook for transaction: {TransactionCode}", verifiedData.OrderCode);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing payment webhook: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}