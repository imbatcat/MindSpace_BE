using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.Feedbacks.CreateFeedbackForPsychologist;

namespace MindSpace.API.Controllers
{
    [Route("api/v{version:apiVersion}/feedbacks")]
    public class FeedbacksController(IMediator mediator) : BaseApiController
    {
        // ====================================
        // === CREATE, PATCH, DELETE, PUT
        // ====================================

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

