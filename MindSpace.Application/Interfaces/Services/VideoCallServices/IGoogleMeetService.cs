using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Interfaces.Services.VideoCallServices
{
    public interface IGoogleMeetService
    {
        Task<string> GetGoogleMeetLink(Appointment appointment);
        Task DeleteMeetingAsync(string meetingId);
    }
}