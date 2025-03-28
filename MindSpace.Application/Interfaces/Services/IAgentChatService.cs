namespace MindSpace.Application.Interfaces.Services
{
    public interface IAgentChatService
    {
        Task<string> GenerateContentAsync(string prompt);

        string GenerateScopedSuggestion(
            IEnumerable<string> listOfSpecialization,
            IEnumerable<string> listOfTestCategory,
            IEnumerable<string> listOfSupportingPrograms,
            IEnumerable<(string psychologistName, string specializationName)> psychologists,
            string prompt);
    }
}
