using Microsoft.AspNetCore.SignalR;
using MindSpace.Application.Notifications;
using System.Collections.Concurrent;

namespace MindSpace.Infrastructure.Services.SignalR;

public class PaymentHub : Hub<IPaymentClientNotification>
{
    // Static dictionary to track connections across hub instances
    private static readonly ConcurrentDictionary<string, string> _connections = new();
    // Dictionary of groups: groupId -> set of connectionIds
    private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, bool>> _groups = new();

    public int ConnectionCount => _connections.Count;

    // Get count of connections in a specific group
    public static int GetGroupConnectionCount(string groupId)
    {
        return _groups.TryGetValue(groupId, out var connections) ? connections.Count : 0;
    }

    public async Task AddToGroup(string sessionId)
    {
        if (!_connections.TryAdd(Context.ConnectionId, sessionId))
        {
            throw new HubException("Failed to add to group");
        }

        // Add connection to the group tracking dictionary
        var groupConnections = _groups.GetOrAdd(sessionId, _ => new ConcurrentDictionary<string, bool>());
        groupConnections.TryAdd(Context.ConnectionId, true);

        await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
    }

    public async Task RemoveFromGroup(string sessionId)
    {
        if (!_connections.TryRemove(Context.ConnectionId, out _))
        {
            throw new HubException("Failed to remove from group");
        }

        // Remove connection from group tracking
        if (_groups.TryGetValue(sessionId, out var groupConnections))
        {
            groupConnections.TryRemove(Context.ConnectionId, out _);

            // If group is empty, remove it from tracking
            if (groupConnections.IsEmpty)
            {
                _groups.TryRemove(sessionId, out _);
            }
        }

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionId);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // Get the session ID for the disconnected client
        if (_connections.TryRemove(Context.ConnectionId, out var sessionId))
        {
            // Remove from group tracking
            if (_groups.TryGetValue(sessionId, out var groupConnections))
            {
                groupConnections.TryRemove(Context.ConnectionId, out _);

                // If group is empty, remove it from tracking
                if (groupConnections.IsEmpty)
                {
                    _groups.TryRemove(sessionId, out _);
                }
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionId);
        }

        await base.OnDisconnectedAsync(exception);
    }
}
