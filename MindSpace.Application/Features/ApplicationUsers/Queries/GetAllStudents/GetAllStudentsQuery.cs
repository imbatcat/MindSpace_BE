using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.GetAllStudents
{
    public class GetAllStudentsQuery : IRequest<PagedResultDTO<ApplicationUserResponseDTO>>
    {
        public ApplicationUserSpecParams SpecParams { get; private set; }

        public GetAllStudentsQuery(ApplicationUserSpecParams specParams)
        {
            SpecParams = specParams;
        }
    }
}