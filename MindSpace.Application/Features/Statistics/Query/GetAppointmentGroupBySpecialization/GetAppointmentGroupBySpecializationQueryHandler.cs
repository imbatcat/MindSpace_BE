using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Statistics.AppointmentStatistics;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.Statistics.Query.GetAppointmentGroupBySpecialization
{
    public class GetAppointmentGroupBySpecializationQueryHandler(ILogger<GetAppointmentGroupBySpecializationQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper) : IRequestHandler<GetAppointmentGroupBySpecializationQuery, AppointmentGroupBySpecializationDTO>
    {
        public async Task<AppointmentGroupBySpecializationDTO> Handle(GetAppointmentGroupBySpecializationQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get statistics of Appointments by specialization group analysis with Spec: {@Spec}", request);
            var specification = new AppointmentSpecification(request.SchoolId, request.StartDate, request.EndDate);

            var appointments = await unitOfWork.Repository<Appointment>().GetAllWithSpecAsync(specification);

            int totalAppointmentCount = appointments.Count;

            IEnumerable<IGrouping<int, Appointment>> groupedData = appointments.GroupBy(a => a.SpecializationId);
            List<AppointmentPairDTO> keyValuePairs = new();
            foreach (var group in groupedData)
            {
                SpecializationDTO specialization = mapper.Map<SpecializationDTO>(group.ToList()[0].Specialization);
                keyValuePairs.Add(new AppointmentPairDTO { Specialization = specialization, AppointmentCount = group.ToList().Count });
            }
            return new AppointmentGroupBySpecializationDTO
            {
                SchoolId = request.SchoolId,
                TotalAppointmentCount = totalAppointmentCount,
                KeyValuePairs = keyValuePairs
            };
        }
    }
}
