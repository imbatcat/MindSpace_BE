using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Commons.Constants;
using MindSpace.Application.DTOs.Chat;
using MindSpace.Application.Interfaces.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Concurrent;

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
            if (string.IsNullOrWhiteSpace(prompt)) return "Câu trả lời không khả dụng. Hãy đặt câu hỏi cho tụi mình nhé.";

            // Impose some rules for agent
            string systemInstruction = $"""
                Bạn là một trợ lý AI chuyên về tâm lý học. Câu trả lời của bạn phải tập trung vào các lĩnh vực sau:  
                - **Chuyên ngành:** {string.Join(", ", AppCts.AiChatKeywords.SpecificationsKeywords)}  
                - **Bài kiểm tra tâm lý:** {string.Join(", ", AppCts.AiChatKeywords.PsychologicalKeywords)}  
                - **Chương trình hỗ trợ:** {string.Join(", ", AppCts.AiChatKeywords.SupportingPrograms)}  

                ### **Hướng dẫn trả lời:**  
                **Chỉ tập trung vào chủ đề trên**  
                   - Chỉ cung cấp thông tin về các lĩnh vực tâm lý học đã liệt kê.  
                   - Nếu câu hỏi nằm ngoài phạm vi này, hãy lịch sự từ chối và yêu cầu người dùng đặt lại câu hỏi phù hợp.  

                **Ngắn gọn, rõ ràng**  
                   - Giới hạn câu trả lời trong **3-4 câu**, đảm bảo súc tích nhưng đủ ý.  
                   - Không lan man hoặc đề cập đến thông tin không liên quan.  

                **Luôn trả lời bằng tiếng Việt**  
                   - Bất kể câu hỏi bằng ngôn ngữ nào, câu trả lời luôn phải bằng tiếng Việt.
                   - Luôn tìm top 3 đáp án trong danh sách dưới đây và gợi ý cho người dùng và liệt kê dưới dạng bulletpoint.

                **Câu hỏi phải nằm trong câu trả lời sau đây**
                   - Chuyên ngành: {string.Join(", ", listOfSpecialization)}
                   - Bài kiểm tra tâm lý: {string.Join(", ", listOfTestCategory)}
                   - Chương trình hỗ trợ: {string.Join(", ", listOfSupportingPrograms)}

                **Câu hỏi của người dùng:** {prompt}  

                **Trả lời súc tích:**
            """;

            return systemInstruction;
        }
    }
}
