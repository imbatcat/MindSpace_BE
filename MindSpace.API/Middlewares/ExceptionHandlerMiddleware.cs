
namespace MindSpace.API.Middlewares
{
    public class ExceptionHandlerMiddleware(
        ILogger<ExceptionHandlerMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            } 
            catch (Exception ex)
            {
                logger.LogCritical(ex.Message);
            }
        }
    }
}
