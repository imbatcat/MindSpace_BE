using MediatR;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.GetAllPsychologistsNames
{
    public class GetAllPsychologistsNamesQuery : IRequest<List<string>>
    {
    }
}
