using MediatR;

namespace MindSpace.Application.Features.Appointments.Queries.GetSessionUrl
{
    public class GetSessionUrlQuery : IRequest<string>
    {
        public required string SessionId { get; set; }
    }
}
