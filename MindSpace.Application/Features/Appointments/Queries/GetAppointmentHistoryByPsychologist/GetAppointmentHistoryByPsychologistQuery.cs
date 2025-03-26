using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Specifications.AppointmentSpecifications;

namespace MindSpace.Application.Features.Appointments.Queries.GetAppointmentHistoryByPsychologist;

public class GetAppointmentHistoryByPsychologistQuery : IRequest<PagedResultDTO<PsychologistAppointmentHistoryDTO>>
{
    public AppointmentSpecParamsForPsychologist SpecParams { get; private set; }

    public GetAppointmentHistoryByPsychologistQuery(AppointmentSpecParamsForPsychologist specParams)
    {
        this.SpecParams = specParams;
    }
}