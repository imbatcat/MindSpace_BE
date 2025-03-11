namespace MindSpace.Application.Interfaces.Services
{
    public interface IResponseCachingService
    {
        // Store data in cache with expiration time when retrieving response
        Task StoreDataInCacheAsync(string key, object responseData, TimeSpan expirationTime);

        // Retrieve data from cache
        Task<string?> GetDataFromCacheAsync(string key);

        // Remove all cach entries matching a pattern (e.g., "products*")
        Task RemoveDataInCacheByPatternAsync(string pattern);
    }
}
