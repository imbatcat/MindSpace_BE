using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;

namespace MindSpace.Application.Features.PsychologistSchedules.Commands
{
    public class UpdatePsychologistScheduleCommandHandler : IRequestHandler<UpdatePsychologistScheduleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePsychologistScheduleCommandHandler> _logger;

        public UpdatePsychologistScheduleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdatePsychologistScheduleCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public Task Handle(UpdatePsychologistScheduleCommand request, CancellationToken cancellationToken)
        {
            // from start date to end date
                // add each slot to the database
            //for (var date = request.StartDate; date <= request.EndDate; date = date.AddDays(1))
            //{
                
            //}
            throw new NotSupportedException();
        }
    }
}
