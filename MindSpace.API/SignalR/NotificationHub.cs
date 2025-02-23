using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace MindSpace.API.SignalR
{
    public class NotificationHub : Hub
    {

        // ==============================
        // === Props & Fields
        // ==============================

        // email - connection string
        private static readonly ConcurrentDictionary<string, string> UserConnection = new();

        // ==============================
        // === Methods
        // ==============================

        public override Task OnConnectedAsync()
        {
            var userEmail = Context.User?.FindFirst(ClaimTypes.Email).Value;
            if (!string.IsNullOrEmpty(userEmail))
            {
                UserConnection.AddOrUpdate(userEmail, Context.ConnectionId,
                                            (key, oldConnection) => Context.ConnectionId);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var email = Context.User?.FindFirst(ClaimTypes.Email).Value;
            if (!string.IsNullOrEmpty(email))
            {
                UserConnection.TryRemove(email, out _);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public static string? GetConnectionIdByEmail(string email)
        {
            UserConnection.TryGetValue(email, out string connectionId);
            return connectionId;
        }
    }
}
