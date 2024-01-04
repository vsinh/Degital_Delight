using Microsoft.AspNetCore.Mvc;

namespace DegitalDelight.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
