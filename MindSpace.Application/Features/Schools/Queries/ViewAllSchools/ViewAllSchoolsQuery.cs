using MediatR;
using MindSpace.Application.DTOs;

namespace MindSpace.Application.Features.Schools.Queries.ViewAllSchools
{
    public class ViewAllSchoolsQuery : IRequest<List<SchoolDTO>>
    {
    }
}