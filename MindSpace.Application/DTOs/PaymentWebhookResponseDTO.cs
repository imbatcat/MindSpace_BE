namespace MindSpace.Application.DTOs;

public class PaymentWebhookResponseDTO
{
    public string OrderCode { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public long Amount { get; set; }
    public DateTime TransactionTime { get; set; }
}