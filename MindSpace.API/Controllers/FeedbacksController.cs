using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.Features.Feedbacks.CreateFeedbackForPsychologist;

namespace MindSpace.API.Controllers
{
    [Route("api/v{version:apiVersion}/feedbacks")]
    public class FeedbacksController(IMediator mediator) : BaseApiController
    {
        // POST /api/v1/feedbacks
        // Create a new feedback for a psychologist
        [HttpPost]
        public async Task<ActionResult> CreateFeedBackForPsychologist(
            [FromBody] CreateFeedbackForPsychologistCommand createFeedbackForPsychologistCommand)
        {
            await mediator.Send(createFeedbackForPsychologistCommand);
            return NoContent();
        }
    }
}

