using MediatR;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.DTOs;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;


namespace MindSpace.Application.Features.AppointmentNotes.Queries.GetAppointmentNotesByAccount
{
    public class GetAppointmentNotesByAccountQueryHandler : IRequestHandler<GetAppointmentNotesByAccountQuery, PagedResultDTO<AppointmentNotesDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAppointmentNotesByAccountQueryHandler> _logger;

        // constructor
        public GetAppointmentNotesByAccountQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GetAppointmentNotesByAccountQueryHandler> logger
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PagedResultDTO<AppointmentNotesDTO>> Handle(GetAppointmentNotesByAccountQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get list of appointment notes with Spec: {@Spec}", request.Params);
            var appointments = await _unitOfWork.Repository<Appointment>()
                .GetAllWithSpecAsync(new AppointmentSpecification(request.Params));
            List<AppointmentNotesDTO> dtos = new List<AppointmentNotesDTO>();
            foreach (Appointment appointment in appointments)
            {
                AppointmentNotesDTO dto = new AppointmentNotesDTO
                {
                    AppointmentId = appointment.Id,
                    NotesTitle = appointment.NotesTitle,
                    KeyIssues = appointment.KeyIssues,
                    Suggestions = appointment.Suggestions,
                    OtherNotes = appointment.OtherNotes,
                    IsNoteShown = appointment.IsNoteShown,
                    PsychologistName = appointment.Psychologist.FullName,
                    PsychologistId = appointment.PsychologistId,
                    StudentId = appointment.StudentId,
                    StudentName = appointment.Student.FullName,
                    PsychologistImageUrl = appointment.Psychologist.ImageUrl,
                    StudentImageUrl = appointment.Student.ImageUrl

                };
                dtos.Add(dto);
            }

            int count = await _unitOfWork.Repository<Appointment>()
                .CountAsync(new AppointmentSpecification(request.Params));
            return new PagedResultDTO<AppointmentNotesDTO>(count, dtos);
        }
    }
}
