using MindSpace.Application.DTOs;
using MindSpace.Domain.Entities.Appointments;
using Net.payOS.Types;

namespace MindSpace.Application.Interfaces.Services.PaymentServices;

public interface IPaymentService
{
    Task<CreatePaymentResult> CreatePaymentLinkAsync(
        int amount,
        string description,
        List<ItemData> items,
        int appointmentId,
        int accountId);
    Task CancelPaymentLinkAsync(int paymentId);
    Task<Invoice> SavePaymentAsync(
        int appointmentId,
        int accountId,
        int amount,
        int transactionCode,
        string description);
    Task<PaymentWebhookResponseDTO> VerifyWebhookDataAsync(WebhookType webhookData);
    Task UpdatePaymentFromWebhookAsync(Invoice payment, PaymentWebhookResponseDTO webhookData);
}