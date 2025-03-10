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

        public string GenerateScopedSuggestion(IEnumerable<string> listOfLimitations, string prompt)
        {
            // If null then just get prompt from user
            if (listOfLimitations == null || !listOfLimitations.Any()) return prompt;

            // Impose some rules for agent
            string limitations = string.Join("\n- ", listOfLimitations);
            string systemInstruction = $"""
                You are an AI assistant specializing in the following psychology domains:
                - {limitations}

                **Response Guidelines:**
                - Your response must be **strictly limited** to these psychology specializations.
                - If the user’s query is **outside these topics**, politely decline and request a refined query.
                - Keep your response **concise (max 3-4 sentences)** while ensuring clarity.
                - Avoid unnecessary details and stay relevant.

                **User Query:** {prompt}
                **Your concise response:** 
               """;

            return systemInstruction;
        }
    }
}
