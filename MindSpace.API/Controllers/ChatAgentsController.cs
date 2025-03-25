using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Chat;
using MindSpace.Application.Features.ChatAgents.Commands;
using MindSpace.Application.Interfaces.Services;

namespace MindSpace.API.Controllers
{
    [Route("api/v{version:apiVersion}/chat-agents")]
    public class ChatAgentsController(
        IMediator mediator,
        IAgentChatService agentChatService
        ) : BaseApiController
    {
        // ==============================
        // === POST, PUT, DELETE, PATCH
        // ==============================

        // POST /api/v1/chat-agents/generate
        [HttpPost("generate")]
        public async Task<ActionResult<ChatResponseDTO>> GenerateContentAsync(
            [FromBody] GenerateChatContentCommand chatAgentCommand)
        {
            var result = await mediator.Send(chatAgentCommand);
            return Ok(result);
        }
    }
}
