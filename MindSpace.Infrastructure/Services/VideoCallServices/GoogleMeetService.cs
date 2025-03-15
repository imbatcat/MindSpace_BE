using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services.VideoCallServices;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Infrastructure.Services.VideoCallServices
{
    public class GoogleMeetService(
        ILogger<GoogleMeetService> _logger
    ) : IGoogleMeetService
    {
        private readonly CalendarService _calendarService = new();

        public async Task DeleteMeetingAsync(string meetingId)
        {
            var request = _calendarService.Events.Delete("primary", meetingId);
            await request.ExecuteAsync();
        }

        public async Task<string> GetGoogleMeetLink(Appointment appointment)
        {
            var startTime = appointment.PsychologistSchedule.Date.ToDateTime(appointment.PsychologistSchedule.StartTime);
            var endTime = appointment.PsychologistSchedule.Date.ToDateTime(appointment.PsychologistSchedule.EndTime);
            try
            {
                var meetEvent = new Event
                {
                    Summary = $"MindSpace: {appointment.Id}",
                    Description = $"MindSpace: {appointment.Id}",
                    Start = new EventDateTime
                    {
                        DateTime = startTime,
                        TimeZone = "Asia/Ho_Chi_Minh"
                    },
                    End = new EventDateTime
                    {
                        DateTime = endTime,
                        TimeZone = "Asia/Ho_Chi_Minh"
                    },
                    Attendees =
                    [
                        new EventAttendee { Email = appointment.Student.Email },
                        new EventAttendee { Email = appointment.Psychologist.Email }
                    ],
                    ConferenceData = new ConferenceData
                    {
                        CreateRequest = new CreateConferenceRequest
                        {
                            RequestId = Guid.NewGuid().ToString(),
                            ConferenceSolutionKey = new ConferenceSolutionKey
                            {
                                Type = "hangoutsMeet"
                            }
                        }
                    }
                };
                var request = _calendarService.Events.Insert(meetEvent, "primary");
                request.ConferenceDataVersion = 1;
                var response = await request.ExecuteAsync();

                return response.HangoutLink;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Google Meet event");
                throw;
            }
        }
    }
}