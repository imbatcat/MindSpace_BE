using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Interfaces.Services.VideoCallServices;

public interface IWebRTCService
{
    /// <summary>
    /// Creates a new WebRTC room for an appointment
    /// </summary>
    /// <param name="appointment">The appointment for which to create a room</param>
    /// <returns>The URL for the WebRTC room</returns>
    (string roomId, string roomUrl) CreateRoom(Appointment appointment);
    /// <summary>
    /// Deletes a WebRTC room
    /// </summary>
    /// <param name="roomId">The ID of the room to delete</param>
    void DeleteRoom(string roomId);
    /// <summary>
    /// Validates if a room is active
    /// </summary>
    /// <param name="roomId">The ID of the room to validate</param>
    /// <returns>True if the room is active, false otherwise</returns>
    bool IsRoomActive(string roomId);

    /// <summary>
    /// Extends the lifetime of a room by a set number of minutes
    /// </summary>
    /// <param name="roomId">The ID of the room to extend</param>
    void ExtendRoomLifetime(string roomId);
}
