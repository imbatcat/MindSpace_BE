using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.Appointments.Queries.GetAppointmentsWithUserId;

internal class GetAppointmentsWithUserEmailQueryHandler(
    IUnitOfWork _unitOfWork,
    ILogger<GetAppointmentsWithUserEmailQueryHandler> _logger,
    IMapper _mapper,
    IUserContext _userContext
) : IRequestHandler<GetAppointmentHistoryByUserQuery, PagedResultDTO<AppointmentHistoryDTO>>
{
    public async Task<PagedResultDTO<AppointmentHistoryDTO>> Handle(GetAppointmentHistoryByUserQuery request, CancellationToken cancellationToken)
    {
        var userClaims = _userContext.GetCurrentUser();
        _logger.LogInformation("Getting appointments for user {UserEmail}", userClaims!.Email);
        var specification = new AppointmentSpecification(request.SpecParams, userClaims!.Email);
        try
        {
            var appointmentDTOs = await _unitOfWork.Repository<Appointment>().GetAllWithSpecProjectedAsync<AppointmentHistoryDTO>(specification, _mapper.ConfigurationProvider);

            var count = await _unitOfWork.Repository<Appointment>().CountAsync(specification);
            appointmentDTOs = appointmentDTOs.Select(x =>
            {
                var dateCompareResult = x.Date.CompareTo(DateOnly.FromDateTime(DateTime.Now));

                if ((dateCompareResult == 0 && x.EndTime > TimeOnly.FromDateTime(DateTime.Now)) || (dateCompareResult > 0))
                {
                    x.IsUpcoming = true;
                }
                else x.IsUpcoming = false;
                return x;
            }).ToList().AsReadOnly();
            _logger.LogInformation("Found {Count} appointments for user {UserEmail}", appointmentDTOs.Count, userClaims!.Email);
            return new PagedResultDTO<AppointmentHistoryDTO>(count, appointmentDTOs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting appointments for user {UserEmail}", userClaims!.Email);
            throw;
        }
    }
}
