using Microsoft.AspNetCore.Http;

namespace MindSpace.Domain.Services.Authentication
{
    public interface IExcelReaderService
    {
        Task<List<Dictionary<string, string>>> ReadExcelAsync(IFormFile file);
    }
}