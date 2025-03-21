using MindSpace.Domain.Entities.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MindSpace.Application.Specifications.MeetingRoomSpecifications
{
    public class MeetingRoomSpecification : BaseSpecification<MeetingRoom>
    {
        public MeetingRoomSpecification(DateTime startDate) :
            base(
                x => x.StartDate.Date == startDate.Date &&
                x.StartDate.TimeOfDay <= TimeSpan.FromHours(23) + TimeSpan.FromMinutes(59) + TimeSpan.FromSeconds(59) &&
                x.StartDate.TimeOfDay >= TimeSpan.Zero
            )
        {
        }

        public MeetingRoomSpecification(string roomId) :
            base(x => string.Compare(roomId, x.RoomId, StringComparison.OrdinalIgnoreCase) == 0)
        {
        }
    }
}