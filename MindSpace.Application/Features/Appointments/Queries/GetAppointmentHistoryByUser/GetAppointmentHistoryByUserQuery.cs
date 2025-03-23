using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Specifications.AppointmentSpecifications;

namespace MindSpace.Application.Features.Appointments.Queries.GetAppointmentHistoryByUser;

public class GetAppointmentHistoryByUserQuery : IRequest<PagedResultDTO<AppointmentHistoryDTO>>
{
    public AppointmentSpecParams SpecParams { get; private set; }

    public GetAppointmentHistoryByUserQuery(AppointmentSpecParams SpecParams)
    {
        this.SpecParams = SpecParams;
    }
}
