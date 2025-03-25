using Microsoft.AspNetCore.Mvc.Filters;
using MindSpace.Application.Interfaces.Services;

namespace MindSpace.API.RequestHelpers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class InvalidateCacheAttribute(string pattern) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 1. BEFORE executing the action

            // 2. DURING the action execution
            // - Currently processing DELETE, PUT, POST, ...
            var responseFromDatabase = await next();

            // 3. AFTER executing the action
            // - For POST, DELETE, PUT,.. if successful or unsuccessful
            // - Always delete record in Redis to maintain the data integrity
            if (responseFromDatabase.Exception == null || responseFromDatabase.ExceptionHandled)
            {
                var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCachingService>();
                await cacheService.RemoveDataInCacheByPatternAsync(pattern);
            }
        }
    }
}
