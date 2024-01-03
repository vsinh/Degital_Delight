using Microsoft.AspNetCore.Mvc;

namespace DegitalDelight.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        [Area("Admin")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
