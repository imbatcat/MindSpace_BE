using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services.PaymentServices;

namespace MindSpace.Application.Features.Appointments.Queries.GetSessionUrl
{
    internal class GetSessionUrlQueryHandler(
        ILogger<GetSessionUrlQueryHandler> _logger,
        IStripePaymentService _stripePaymentService) : IRequestHandler<GetSessionUrlQuery, string>
    {
        public async Task<string> Handle(GetSessionUrlQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving session url for session id: {id}", request.SessionId);

            var sessionUrl = await _stripePaymentService.RetrieveSessionUrlAsync(request.SessionId);

            _logger.LogInformation("URL retrieved for session id: {id} - {url}", request.SessionId, sessionUrl);
            return sessionUrl;
        }
    }
}
