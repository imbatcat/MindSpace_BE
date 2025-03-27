using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.Features.Feedbacks.CreateFeedbackForPsychologist;

namespace MindSpace.API.Controllers
{
    public class FeedbacksController(IMediator mediator) : BaseApiController
    {
        // POST: api/v1/feedbacks
        [HttpPost]
        public async Task<ActionResult> CreateFeedBackForPsychologist(
            [FromBody] CreateFeedbackForPsychologistCommand createFeedbackForPsychologistCommand)
        {
            await mediator.Send(createFeedbackForPsychologistCommand);
            return NoContent();
        }
    }
}

