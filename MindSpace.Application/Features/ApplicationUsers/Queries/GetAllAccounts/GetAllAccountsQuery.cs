using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.GetAllAccounts
{
    public class GetAllAccountsQuery : IRequest<PagedResultDTO<ApplicationUserResponseDTO>>
    {
        public ApplicationUserSpecParams SpecParams { get; private set; }

        public GetAllAccountsQuery(ApplicationUserSpecParams specParams)
        {
            SpecParams = specParams;
        }
    }
}