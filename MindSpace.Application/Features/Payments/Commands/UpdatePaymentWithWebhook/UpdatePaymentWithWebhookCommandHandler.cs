using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Specifications.PaymentSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Payments.Commands.UpdatePaymentWithWebhook;

public class UpdatePaymentWithWebhookCommandHandler(
    ILogger<UpdatePaymentWithWebhookCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IPaymentService paymentService,
    ISignalRNotification signalRNotification
    ) : IRequestHandler<UpdatePaymentWithWebhookCommand>
{
    public async Task Handle(UpdatePaymentWithWebhookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Processing payment webhook: {WebhookPayload}", request.Data.data.accountNumber);

            var verifiedData = await paymentService.VerifyWebhookDataAsync(request.Data);

            var specification = new PaymentSpecification(int.Parse(verifiedData.OrderCode));
            var payment = await unitOfWork.Repository<Invoice>().GetBySpecAsync(specification);

            if (payment == null)
            {
                logger.LogError("Payment not found for transaction code: {TransactionCode}", verifiedData.OrderCode);
                throw new NotFoundException(nameof(Invoice), verifiedData.OrderCode);
            }

            await paymentService.UpdatePaymentFromWebhookAsync(payment, verifiedData);
            await unitOfWork.CompleteAsync();

            // Update Appointment Status
            // Add to Invoice Table
            // Update Schedules Status (Demo)
            PsychologistSchedule schedule = new PsychologistSchedule()
            {
                Status = PsychologistScheduleStatus.Booked
            };

            // Notify all connections subscribe to clients about the status's changes
            await signalRNotification.NotifyScheduleStatus(schedule);

            logger.LogInformation("Successfully processed payment webhook for transaction: {TransactionCode}", verifiedData.OrderCode);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing payment webhook: {ErrorMessage}", ex.Message);
            throw;
        }
    }
}