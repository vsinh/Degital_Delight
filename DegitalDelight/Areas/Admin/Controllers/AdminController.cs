using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DegitalDelight.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        [Area("Admin")]
		[Authorize(Roles = "Administrator")]
		public IActionResult Admin()
        {
            return View();
        }
    }
}
