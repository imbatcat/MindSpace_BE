using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services.VideoCallServices;
using MindSpace.Application.Notifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Infrastructure.Services.SignalR;
using System.Collections.Concurrent;

namespace MindSpace.Infrastructure.Services.VideoCallServices;

public class WebRTCService(
    ILogger<WebRTCService> _logger,
    IHubContext<WebRTCHub, IWebRTCClientNotification> _hubContext,
    IConfiguration _configuration
) : IWebRTCService
{
    private static readonly ConcurrentDictionary<string, DateTime> _activeRooms = new();
    private readonly string _frontendUrl = _configuration.GetValue<string>("FrontendUrl") ?? throw new InvalidOperationException("FrontendUrl is not configured");
    private readonly int _roomExpirationMinutes = _configuration.GetValue<int>("RoomExpirationMinutes");

    public static ConcurrentDictionary<string, DateTime> ActiveRooms => _activeRooms;
    public (string roomId, string roomUrl) CreateRoom(Appointment appointment)
    {
        _logger.LogInformation("Creating room for appointment {AppointmentId}", appointment.Id);

        var roomId = $"room_{Guid.NewGuid()}";

        var roomUrl = $"{_frontendUrl}/video-call/{roomId}";

        _logger.LogInformation("Room created successfully. Room URL: {RoomUrl}", roomUrl);

        _activeRooms.TryAdd(roomId, DateTime.Now.AddMinutes(_roomExpirationMinutes));

        return (roomId, roomUrl);
    }

    public void DeleteRoom(string roomId)
    {
        try
        {
            _logger.LogInformation("Deleting WebRTC room {RoomId}", roomId);
            _activeRooms.Remove(roomId, out _);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting WebRTC room {RoomId}", roomId);
            throw;
        }
    }

    public void ExtendRoomLifetime(string roomId)
    {
        //TODO: Implement this
        throw new NotImplementedException();
    }

    public bool IsRoomActive(string roomId)
    {
        try
        {
            _logger.LogInformation("Checking if room {RoomId} is active", roomId);
            return _activeRooms.TryGetValue(roomId, out var expirationTime) && expirationTime > DateTime.Now;
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "Room {RoomId} does not exists", roomId);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if room {RoomId} is active, room", roomId);
            throw;
        }
    }
}
