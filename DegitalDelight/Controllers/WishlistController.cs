using Microsoft.AspNetCore.Mvc;

namespace DegitalDelight.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Wishlist()
        {
            return View();
        }
    }
}
