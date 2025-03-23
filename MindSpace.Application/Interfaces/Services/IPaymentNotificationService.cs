namespace MindSpace.Application.Interfaces.Services;

public interface IPaymentNotificationService
{
    Task NotifyPaymentSuccess(string sessionId);
}
