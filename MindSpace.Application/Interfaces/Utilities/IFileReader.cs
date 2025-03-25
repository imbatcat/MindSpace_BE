namespace MindSpace.Application.Interfaces.Utilities
{
    public interface IFileReader
    {
        Task<string> ReadFileAsync(string filePath);
    }
}