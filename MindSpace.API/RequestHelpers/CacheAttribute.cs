using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MindSpace.Application.Interfaces.Services;
using System.Drawing;
using System.Text;

namespace MindSpace.API.RequestHelpers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute(int timeToLiveSeconds) : Attribute, IAsyncActionFilter
    {
        /*
            Cache Hit
            Request → Generate Key → Check Cache → Found → Return Cached Data

            Cache Miss
            Request → Generate Key → Check Cache → Not Found → Execute Action → Store Data to Cache
         */
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 1. BEFORE: we check cache
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCachingService>();
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResultFromCacheDb = await cacheService.GetDataFromCacheAsync(cacheKey);

            // If having data, return immediately
            if (!string.IsNullOrEmpty(cacheResultFromCacheDb))
            {
                // Return the result and skip the action execution
                context.Result = new ContentResult()
                {
                    Content = cacheResultFromCacheDb,
                    ContentType = "application/json",
                    StatusCode = 200,
                };
                return;
            }

            // 2. DURING: Execute the action
            var responseFromDatabase = await next();

            // 3. AFTER: Set to Cache Database after executing the action
            // - If Ok but having data, then cache, else ignore
            if (responseFromDatabase.Result is OkObjectResult okObjectResult)
            {
                var valueToCache = okObjectResult.Value;
                if (valueToCache == null) return;

                await cacheService.StoreDataInCacheAsync(cacheKey,
                    valueToCache,
                    TimeSpan.FromSeconds(timeToLiveSeconds));
            }
        }

        // First Request
        // Request: GET /api/products? page = 1 & size = 10 & sort = name
        // Cache Key: "/api/products|page-1|size-10|sort-name"

        // Second Request
        // Request: GET /api/products? categories = 1 & categories = 2
        // Cache Key: "/api/products|categories-1|categories-2"
        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            // api/products
            keyBuilder.Append($"{request.Path}");

            foreach (var param in request.Query.OrderBy(x => x.Key))
            {
                // |pageIndex-1|pageSize-5|sort-name
                keyBuilder.Append($"|{param.Key}-{param.Value}");
            }

            // api/products|pageIndex-1|pageSize-5|sort-name
            return keyBuilder.ToString();
        }
    }
}
