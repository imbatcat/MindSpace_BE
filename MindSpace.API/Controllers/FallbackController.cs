using Microsoft.AspNetCore.Mvc;

namespace MindSpace.API.Controllers
{
    /// <summary>
    /// The controller to match undefined routes
    /// </summary>
    public class FallbackController : Controller
    {
        // GET /api/v1/fallback
        // Fallback route to serve index.html for client-side routing
        [HttpGet]
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "index.html"), "text/HTML");
        }
    }
}