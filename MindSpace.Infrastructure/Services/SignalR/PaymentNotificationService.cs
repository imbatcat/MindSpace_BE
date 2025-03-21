using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Notifications;

namespace MindSpace.Infrastructure.Services.SignalR;

public class PaymentNotificationService(
    IHubContext<PaymentHub, IPaymentClientNotification> _hubContext,
    ILogger<PaymentNotificationService> _logger
) : IPaymentNotificationService
{
    public async Task NotifyPaymentSuccess(string sessionId)
    {
        _logger.LogInformation("Notifying payment success for session {SessionId}", sessionId);
        bool hasConnections = false;
        int maxIterations = 15;

        do
        {
            await Task.Delay(2000);
            maxIterations--;
            await _hubContext.Clients.Group(sessionId).ClientNotifyPaymentSuccess();

            // Get connection count using the static method on the hub
            hasConnections = PaymentHub.GetGroupConnectionCount(sessionId) > 0;
            _logger.LogDebug("Session {SessionId} has {ConnectionCount} connections",
                sessionId, PaymentHub.GetGroupConnectionCount(sessionId));

        } while (!hasConnections && maxIterations > 0);
    }
}
