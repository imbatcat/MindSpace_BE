using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Chat;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Specifications.SpecializationSpecifications;
using MindSpace.Domain.Entities;

namespace MindSpace.Application.Features.ChatAgents.Commands
{
    public class GenerateChatContentCommandHandler(
        IUnitOfWork unitOfWork,
        IAgentChatService agentChatService,
        ILogger<GenerateChatContentCommandHandler> logger
        ) : IRequestHandler<GenerateChatContentCommand, ChatResponseDTO>
    {
        public async Task<ChatResponseDTO> Handle(GenerateChatContentCommand request, CancellationToken cancellationToken)
        {
            // Get all specializations
            var specSpec = new SpecializationSpecification(new SpecializationSpecParams());
            var specializations = await unitOfWork.Repository<Specialization>().GetAllWithSpecAsync(specSpec);
            var specializationNames = specializations.Select(x => x.Name);

            // Generate the prompt context within the specialization
            var restrictedPrompt = agentChatService.GenerateScopedSuggestion(specializationNames, request.Prompt);
            var result = await agentChatService.GenerateContentAsync(restrictedPrompt);

            // Return the response dto
            return new ChatResponseDTO()
            {
                Message = result
            };
        }
    }
}
