using Microsoft.AspNetCore.Mvc;

namespace MindSpace.API.Controllers
{
    /// <summary>
    /// The controller to match undefined routes
    /// </summary>
    public class FallbackController : Controller
    {
        // Redirect to the index.html if unmatching any routes
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "index.html"), "text/HTML");
        }
    }
}