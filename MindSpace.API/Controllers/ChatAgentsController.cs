using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Chat;
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
        // === Methods
        // ==============================

        [HttpPost("generate")]
        public async Task<ActionResult<ChatResponseDTO>> GenerateContentAsync(
            [FromBody] PromptRequestDTO promptRequestDTO)
        {
            var output = await agentChatService.GenerateContentAsync(promptRequestDTO.Prompt);
            var chatReponseDto = new ChatResponseDTO()
            {
                Message = output,
            };
            return Ok(chatReponseDto);
        }
    }
}
