using Microsoft.AspNetCore.Http;

namespace MindSpace.Application.Interfaces.Services.FileReaderServices
{
    public interface IExcelReaderService
    {
        Task<List<Dictionary<string, string>>> ReadExcelAsync(IFormFile file);
        Task<Dictionary<string, List<Dictionary<string, string>>>> ReadAllSheetsAsync(IFormFile file);
    }
}