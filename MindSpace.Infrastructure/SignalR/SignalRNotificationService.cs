using Microsoft.AspNetCore.SignalR;
using MindSpace.API.SignalR;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Infrastructure.SignalR
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
