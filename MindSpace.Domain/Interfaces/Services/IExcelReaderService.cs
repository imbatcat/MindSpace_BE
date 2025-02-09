using Microsoft.AspNetCore.Http;

namespace MindSpace.Domain.Interfaces.Services
{
    public interface IExcelReaderService
    {
        Task<List<Dictionary<string, string>>> ReadExcelAsync(IFormFile file);
    }
}