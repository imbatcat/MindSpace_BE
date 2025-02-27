namespace MindSpace.Domain.Models;

public class PaymentWebhookResponse
{
    public string OrderCode { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public long Amount { get; set; }
    public DateTime TransactionTime { get; set; }
}