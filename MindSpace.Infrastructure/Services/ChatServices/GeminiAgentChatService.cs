using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Chat;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Json;
using System.Text.Json;

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
    }
}
