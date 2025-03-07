using Microsoft.AspNetCore.SignalR;
using MindSpace.Application.Notifications;
using System.Security.Claims;

namespace MindSpace.Infrastructure.Services.SignalR
{
    public class NotificationHub : Hub<IClientNotification>
    {
        // ==============================
        // === Methods
        // ==============================

        public override async Task OnConnectedAsync()
        {
            var user = Context.User;
            if (user == null) return;

            var userRole = user.FindFirst(ClaimTypes.Role).Value;
            if (userRole == null) return;

            await Groups.AddToGroupAsync(Context.ConnectionId, userRole);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = Context.User;
            if (user == null) return;

            var userRole = user.FindFirst(ClaimTypes.Role).Value;
            if (userRole == null) return;

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userRole);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
