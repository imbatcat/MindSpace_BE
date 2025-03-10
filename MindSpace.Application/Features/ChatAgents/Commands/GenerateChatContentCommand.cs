using MediatR;
using MindSpace.Application.DTOs.Chat;

namespace MindSpace.Application.Features.ChatAgents.Commands
{
    public class GenerateChatContentCommand : IRequest<ChatResponseDTO>
    {
        public string Prompt { get; set; }
    }
}
