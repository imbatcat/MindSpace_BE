using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;
using MindSpace.Application.Specifications.AppointmentSpecifications;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.ViewAllAccounts
{
    public class ViewAllAccountsQuery : IRequest<PagedResultDTO<ApplicationUserResponseDTO>>
    {
        public ApplicationUserSpecParams SpecParams { get; private set; }

        public ViewAllAccountsQuery(ApplicationUserSpecParams specParams)
        {
            SpecParams = specParams;
        }
    }
}