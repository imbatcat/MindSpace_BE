using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.Features.AppointmentNotes.Queries.GetAppointmentNotesById
{
    public class GetAppointmentNotesByIdQueryHandler : IRequestHandler<GetAppointmentNotesByIdQuery, AppointmentNotesDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAppointmentNotesByIdQueryHandler> _logger;

        // constructor
        public GetAppointmentNotesByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GetAppointmentNotesByIdQueryHandler> logger
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<AppointmentNotesDTO> Handle(GetAppointmentNotesByIdQuery request, CancellationToken cancellationToken)
        {
            Appointment? appointment = await _unitOfWork.Repository<Appointment>().GetBySpecAsync(new AppointmentSpecification(request.Id));

            if (appointment == null || appointment.Status != AppointmentStatus.Success)
            {
                throw new DirectoryNotFoundException("Appointment Notes not found!");
            }
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
            return dto;
        }
    }
}
