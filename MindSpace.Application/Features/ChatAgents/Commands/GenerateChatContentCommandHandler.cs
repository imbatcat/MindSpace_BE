using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Commons.Constants;
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
            try
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

                // Generate the Content
                var result = await agentChatService.GenerateContentAsync(restrictedPrompt);

                // Format the response
                var finalResponse = GenerateFormattedResponse(result);

                // Return the response dto
                return new ChatResponseDTO()
                {
                    UserName = "Customer Support",
                    Message = finalResponse
                };
            }
            catch (Exception ex)
            {
                return new ChatResponseDTO()
                {
                    UserName = "Customer Support",
                    Message = ChatTemplates.SystemPrompts.ErrorResponse
                };
            }
        }


        public string GenerateFormattedResponse(string response)
        {
            // Check if the response is an error or out-of-scope message then response directly
            if (response.Contains(ChatTemplates.SystemPrompts.ErrorResponse) ||
                response.Contains(ChatTemplates.SystemPrompts.OutOfScopeResponse1) ||
                response.Contains(ChatTemplates.SystemPrompts.OutOfScopeResponse2) ||
                response.Contains(ChatTemplates.SystemPrompts.OutOfScopeResponse3))
            {
                return response;
            }

            // Clean up the response by removing extra whitespace and newlines
            response = response.Trim()
                .Replace("\r\n", "\n")
                .Replace("\n\n", "\n")
                .Replace("**", "");

            // Split into sections
            var sections = response.Split("\n")
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim())
                .ToList();

            // Extract the main sections
            var specializationSection = sections.FirstOrDefault(s => s.Contains("Chuyên ngành phù hợp:"));
            var testSection = sections.FirstOrDefault(s => s.Contains("Bài kiểm tra tâm lý:"));
            var programSection = sections.FirstOrDefault(s => s.Contains("Chương trình hỗ trợ:"));

            // Clean up the sections by removing the headers
            var specialization = specializationSection?.Replace("Chuyên ngành phù hợp:", "").Trim();
            var tests = testSection?.Replace("Bài kiểm tra tâm lý:", "").Trim();
            var programs = programSection?.Replace("Chương trình hỗ trợ:", "").Trim();

            // Format the response using the template
            return string.Format(
                ChatTemplates.ResponseFormats.SuggestionFormat,
                specialization ?? "Không có đề xuất phù hợp",
                tests ?? "Không có đề xuất phù hợp",
                programs ?? "Không có đề xuất phù hợp"
            );
        }
    }
}
