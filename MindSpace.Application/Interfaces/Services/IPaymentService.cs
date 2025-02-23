using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Models;
using Net.payOS.Types;

namespace MindSpace.Application.Interfaces.Services;

public interface IPaymentService
{
    Task<CreatePaymentResult> CreatePaymentLinkAsync(
        int amount,
        string description,
        List<ItemData> items,
        int appointmentId,
        int accountId);
    Task CancelPaymentLinkAsync(int paymentId);
    Task<Payment> SavePaymentAsync(
        int appointmentId,
        int accountId,
        int amount,
        int transactionCode,
        string description);
    Task<PaymentWebhookResponse> VerifyWebhookDataAsync(WebhookType webhookData);
    Task UpdatePaymentFromWebhookAsync(Payment payment, PaymentWebhookResponse webhookData);
}