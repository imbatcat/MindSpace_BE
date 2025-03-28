using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Specifications.AppointmentSpecifications;

namespace MindSpace.Application.Features.Appointments.Queries.GetAppointmentHistoryList
{
    public class GetAppointmentHistoryListQuery : IRequest<PagedResultDTO<AppointmentHistoryDTO>>
    {
        public AppointmentSpecParams SpecParams { get; private set; }

        public GetAppointmentHistoryListQuery(AppointmentSpecParams SpecParams)
        {
            this.SpecParams = SpecParams;
        }
    }
}
