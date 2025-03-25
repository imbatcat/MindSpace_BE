using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services.PaymentServices;
using Net.payOS.Errors;
using Net.payOS.Types;

namespace MindSpace.Application.Features.Payments.Commands.CreatePayment;

public class CreatePaymentCommandHandler(
    ILogger<CreatePaymentCommandHandler> logger,
    IPaymentService paymentService) : IRequestHandler<CreatePaymentCommand, CreatePaymentResult>
{
    public async Task<CreatePaymentResult> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await paymentService.CreatePaymentLinkAsync(
                request.Ammount,
                request.Description,
                request.Items,
                request.AppointmentId,
                request.AccountId);
        }
        catch (PayOSError ex)
        {
            logger.LogError(ex, "Error occurred while creating payment");
            throw;
        }
    }
}