using Microsoft.AspNetCore.SignalR;
using MindSpace.Application.Interfaces.Services.SignalR;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Infrastructure.Services.SignalR
{
    public class SignalRNotificationService : ISignalRNotification
    {
        // ==============================
        // === Props & Fields
        // ==============================

        private readonly IHubContext<NotificationHub> _hubContext;

        // ==============================
        // === Constructors
        // ==============================

        public SignalRNotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // ==============================
        // === Methods
        // ==============================

        public async Task NotifyScheduleStatus(PsychologistSchedule schedule)
        {
            await _hubContext.Clients.All.SendAsync("ScheduleBookedNotification", schedule);
        }
    }
}
