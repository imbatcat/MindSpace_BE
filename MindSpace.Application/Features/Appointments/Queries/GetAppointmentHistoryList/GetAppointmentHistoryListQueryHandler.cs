using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.DTOs;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.Appointments.Queries.GetAppointmentHistoryList
{
    public class GetAppointmentHistoryListQueryHandler(
    IUnitOfWork _unitOfWork,
    ILogger<GetAppointmentHistoryListQueryHandler> _logger,
    IMapper _mapper) : IRequestHandler<GetAppointmentHistoryListQuery, PagedResultDTO<AppointmentHistoryDTO>>
    {
        public async Task<PagedResultDTO<AppointmentHistoryDTO>> Handle(GetAppointmentHistoryListQuery request, CancellationToken cancellationToken)
        {
            var specification = new AppointmentSpecification(request.SpecParams);
            try
            {
                var tpm = await _unitOfWork.Repository<Appointment>().GetAllWithSpecAsync(specification);
                var appointmentDTOs = await _unitOfWork.Repository<Appointment>().GetAllWithSpecProjectedAsync<AppointmentHistoryDTO>(specification, _mapper.ConfigurationProvider);

                var count = await _unitOfWork.Repository<Appointment>().CountAsync(specification);
                appointmentDTOs = appointmentDTOs.Select(x =>
                {
                    var dateCompareResult = x.Date.CompareTo(DateOnly.FromDateTime(DateTime.Now));

                    if (dateCompareResult == 0 && x.EndTime > TimeOnly.FromDateTime(DateTime.Now) || dateCompareResult > 0)
                    {
                        x.IsUpcoming = true;
                    }
                    else x.IsUpcoming = false;
                    return x;
                }).ToList().AsReadOnly();
                _logger.LogInformation("Found {Count} appointments", appointmentDTOs.Count);
                return new PagedResultDTO<AppointmentHistoryDTO>(count, appointmentDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting appointments");
                throw;
            }
        }
    }

}
