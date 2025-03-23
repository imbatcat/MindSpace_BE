using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.Appointments.Queries.GetAppointmentsWithUserEmail;

internal class GetAppointmentsWithUserEmailQueryHandler(
    IUnitOfWork _unitOfWork,
    ILogger<GetAppointmentsWithUserEmailQueryHandler> _logger,
    IMapper _mapper
) : IRequestHandler<GetAppointmentsWithUserEmailQuery, List<AppointmentHistoryDTO>>
{
    public async Task<List<AppointmentHistoryDTO>> Handle(GetAppointmentsWithUserEmailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting appointments for user {UserEmail}", request.UserEmail);
        var specification = new AppointmentSpecification(request.UserEmail, AppointmentSpecification.StringParameterType.StudentEmail);
        try
        {
            var appointmentDTOs = await _unitOfWork.Repository<Appointment>().GetAllWithSpecProjectedAsync<AppointmentHistoryDTO>(specification, _mapper.ConfigurationProvider);

            _logger.LogInformation("Found {Count} appointments for user {UserEmail}", appointmentDTOs.Count, request.UserEmail);
            return appointmentDTOs.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting appointments for user {UserEmail}", request.UserEmail);
            throw;
        }
    }
}
