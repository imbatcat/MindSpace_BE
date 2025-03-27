using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Chat;
using MindSpace.Application.Interfaces.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MindSpace.Infrastructure.Services.ChatServices
{
    public class GeminiAgentChatService : IAgentChatService
    {
        // ==============================
        // === Fields & Props
        // ==============================

        private readonly string _apiKey;
        private readonly string _apiUrl;
        private readonly HttpClient _httpClient;
        private readonly ILogger<GeminiAgentChatService> _logger;

        // ==============================
        // === Constructors 
        // ==============================

        public GeminiAgentChatService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<GeminiAgentChatService> logger)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Gemini:ApiKey"] ?? throw new ArgumentNullException("Gemini key not found");
            _apiUrl = configuration["Gemini:Url"] ?? throw new ArgumentNullException("Gemini Url not found");
            _logger = logger;
        }

        // ==============================
        // === Methods
        // ==============================

        public async Task<string> GenerateContentAsync(string prompt)
        {
            string url = $"{_apiUrl}?key={_apiKey}";

            // Fetch relevant a

            // Request Object for Gemini Prompting
            var request = new ContentRequest()
            {
                contents = new[]
                {
                    new Content()
                    {
                        Parts = new[]
                        {
                            new Part()
                            {
                                Text = prompt
                            }
                        }
                    }
                }
            };

            // Serialize the request object into prompt
            // Camel case property name
            var jsonObjectRequest = JsonConvert.SerializeObject(request, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            });

            var content = new StringContent(jsonObjectRequest, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);

            // If successful, then deserialize an object
            if (response.IsSuccessStatusCode)
            {
                var jsonObjectResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ContentResponse>(jsonObjectResponse);
                if (result == null) throw new Exception("Result returned failed. Please try again");

                return result.Candidates[0].Content.Parts[0].Text;
            }
            else
            {
                throw new Exception("Error communicating with Gemini API.");
            }
        }

        public string GenerateScopedSuggestion(
            IEnumerable<string> listOfSpecialization,
            IEnumerable<string> listOfTestCategory,
            IEnumerable<string> listOfSupportingPrograms,
            IEnumerable<(string psychologistName, string specializationName)> psychologists,
            string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt)) return "Invalid query. Please provide a valid question.";

            var focusAreas = new List<string>();

            // Check if the prompt contains keywords from specializations
            var matchedSpecializations = listOfSpecialization
                .Where(spec => prompt.Contains(spec, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matchedSpecializations.Any())
                focusAreas.Add($"Specialization Areas: {string.Join(", ", matchedSpecializations)}");

            // Check if the prompt contains keywords from test categories
            var matchedTestCategories = listOfTestCategory
                .Where(test => prompt.Contains(test, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matchedTestCategories.Any())
                focusAreas.Add($"Psychological Tests: {string.Join(", ", matchedTestCategories)}");

            // Check if the prompt contains keywords from supporting programs
            var matchedSupportingPrograms = listOfSupportingPrograms
                .Where(prog => prompt.Contains(prog, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matchedSupportingPrograms.Any())
                focusAreas.Add($"Supporting Programs: {string.Join(", ", matchedSupportingPrograms)}");

            // Build focus summary
            string focusSummary = focusAreas.Any()
                ? $"This response should focus on: {string.Join(" | ", focusAreas)}."
                : "Ensure responses remain relevant to all psychology domains.";

            // Impose some rules for agent
            string systemInstruction = $"""
                You are an AI assistant specializing in the following psychology domains:
                - {focusSummary}

                **Response Guidelines:**
                - Your response must be **strictly limited** to these psychology specializations.
                - If the user’s query is **outside these topics**, politely decline and request a refined query.
                - Keep your response **concise (max 3-4 sentences)** while ensuring clarity.
                - Your reponses must be in Vietnamese. Not matter the prompting using Vietnamese or English.
                - Avoid unnecessary details and stay relevant.

                **User Query:** {prompt}
                **Your concise response:** 
               """;

            return systemInstruction;
        }
    }
}
