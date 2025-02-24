using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.PsychologistScheduleSpecifications;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.PsychologistSchedules.Queries
{
    public class GetPsychologistScheduleQueryHandler : IRequestHandler<GetPsychologistScheduleQuery, PagedResultDTO<PsychologistScheduleResponseDTO>>
    {
        // props and fields
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private ILogger _logger;

        // constructors
        public GetPsychologistScheduleQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetPsychologistScheduleQueryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // methods
        public async Task<PagedResultDTO<PsychologistScheduleResponseDTO>> Handle(GetPsychologistScheduleQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get list of Psychologist Schedules with Spec: {@Spec}", request.SpecParams);
            var spec = new PsychologistScheduleSpecification(request.SpecParams);

            var listDtos = await _unitOfWork
                                  .Repository<PsychologistSchedule>()
                                  .GetAllWithSpecProjectedAsync<PsychologistScheduleResponseDTO>(spec, _mapper.ConfigurationProvider);

            var count = await _unitOfWork
                                  .Repository<PsychologistSchedule>()
                                  .CountAsync(spec);

            return new PagedResultDTO<PsychologistScheduleResponseDTO>(count, listDtos);
        }
    }
}
