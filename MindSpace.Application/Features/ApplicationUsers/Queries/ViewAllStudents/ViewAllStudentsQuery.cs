using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.ViewAllStudents
{
    public class ViewAllStudentsQuery : IRequest<PagedResultDTO<ApplicationUserResponseDTO>>
    {
        public ApplicationUserSpecParams SpecParams { get; private set; }

        public ViewAllStudentsQuery(ApplicationUserSpecParams specParams)
        {
            SpecParams = specParams;
        }
    }
}