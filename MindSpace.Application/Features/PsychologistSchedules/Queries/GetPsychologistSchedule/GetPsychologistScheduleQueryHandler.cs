using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.PsychologistScheduleSpecifications;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.PsychologistSchedules.Queries.GetPsychologistSchedule
{
    public class GetPsychologistScheduleQueryHandler : IRequestHandler<GetPsychologistScheduleQuery, IReadOnlyList<PsychologistScheduleResponseDTO>>
    {
        // props and fields
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private IApplicationUserRepository _applicationUserService;
        private ILogger _logger;

        // constructors
        public GetPsychologistScheduleQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetPsychologistScheduleQueryHandler> logger, IApplicationUserRepository applicationUserService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _applicationUserService = applicationUserService;
        }

        // methods
        public async Task<IReadOnlyList<PsychologistScheduleResponseDTO>> Handle(GetPsychologistScheduleQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get list of Psychologist Schedules with Spec: {@Spec}", request.SpecParams);
            
            var spec = new PsychologistScheduleSpecification(request.SpecParams);

            var listTimeSlots = await _unitOfWork
                                  .Repository<PsychologistSchedule>()
                                  .GetAllWithSpecAsync(spec);

            // Group timeslots by date
            var listDtos = listTimeSlots
                .GroupBy(x => x.Date)
                .Select(group => new PsychologistScheduleResponseDTO
                {
                    PsychologistId = group.First().PsychologistId,
                    Date = group.Key.ToString("yyyy-MM-dd"), // Convert DateOnly to string
                    WeekDay = group.Key.DayOfWeek.ToString(),
                    TimeSlots = group.OrderBy(ps => ps.StartTime)
                                    .Select(ps => new TimeSlotDTO
                                    {
                                        Id = ps.Id,
                                        StartTime = ps.StartTime.ToString("HH:mm"),
                                        EndTime = ps.EndTime.ToString("HH:mm"),
                                        Date = ps.Date.ToString("yyyy-MM-dd"),
                                        PsychologistId = ps.PsychologistId,
                                        Status = ps.Status
                                    }).ToList()
                })
                .ToList();

            return listDtos;
        }
    }
}
