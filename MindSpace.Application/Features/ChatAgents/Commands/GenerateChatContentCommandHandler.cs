using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Chat;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Specifications.PsychologistsSpecifications;
using MindSpace.Application.Specifications.SpecializationSpecifications;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Features.ChatAgents.Commands
{
    public class GenerateChatContentCommandHandler(
        IUnitOfWork unitOfWork,
        IAgentChatService agentChatService,
        IApplicationUserService<Psychologist> applicationUserService,
        ILogger<GenerateChatContentCommandHandler> logger
        ) : IRequestHandler<GenerateChatContentCommand, ChatResponseDTO>
    {
        public async Task<ChatResponseDTO> Handle(GenerateChatContentCommand request, CancellationToken cancellationToken)
        {
            // Get all specializations
            var specSpec = new SpecializationSpecification(new SpecializationSpecParams());
            var specializations = await unitOfWork.Repository<Specialization>().GetAllWithSpecAsync(specSpec);
            var specializationNames = specializations.Select(x => x.Name);

            // Get all test categories
            var specTestCategory = new TestCategorySpecification();
            var testCategories = await unitOfWork.Repository<TestCategory>().GetAllWithSpecAsync(specTestCategory);
            var testCategoryNames = testCategories.Select(x => x.Name);

            // Get all supporting programs
            var specSupportingProgram = new SupportingProgramSpecification();
            var supportingPrograms = await unitOfWork.Repository<SupportingProgram>().GetAllWithSpecAsync(specSupportingProgram);
            var supportingProgramNames = supportingPrograms.Select(x => x.Title);

            // Get all psychologists
            var spec = new PsychologistSpecification();
            var psychologists = await applicationUserService.GetAllUsersWithSpecAsync(spec);
            var tuplePsychologists = psychologists.Select(p => (p.FullName, p.Specialization.Name));

            // Generate the prompt based on context
            var restrictedPrompt = agentChatService.GenerateScopedSuggestion(
                specializationNames,
                testCategoryNames,
                supportingProgramNames,
                tuplePsychologists,
                request.Prompt);

            var result = await agentChatService.GenerateContentAsync(restrictedPrompt);

            // Return the response dto
            return new ChatResponseDTO()
            {
                Message = result,
            };
        }
    }
}
