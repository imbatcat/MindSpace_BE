namespace MindSpace.Application.Interfaces.Services
{
    public interface IAgentChatService
    {
        Task<string> GenerateContentAsync(string prompt);

        string GenerateScopedSuggestion(IEnumerable<string> listOfLimitations, string prompt);
    }
}
