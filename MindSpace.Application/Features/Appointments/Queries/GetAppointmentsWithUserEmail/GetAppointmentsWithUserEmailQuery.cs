using MediatR;
using MindSpace.Application.DTOs.Appointments;

namespace MindSpace.Application.Features.Appointments.Queries.GetAppointmentsWithUserEmail;

public class GetAppointmentsWithUserEmailQuery : IRequest<List<AppointmentHistoryDTO>>
{
    public string UserEmail { get; set; }
}
