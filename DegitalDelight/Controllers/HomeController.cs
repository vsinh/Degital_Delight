using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Services;
using DegitalDelight.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace DegitalDelight.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _context = context;
            _productService = productService;
            _cartService = cartService;
        }
      
        public async Task<IActionResult> Homepage()
        {
            return View(await _productService.GetProducts());
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Order()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Favorite()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ViewCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                var cartItems = await _cartService.GetCartItems(userId);
                return View(cartItems);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        public IActionResult Search()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/NotFound")]
        public IActionResult Page404()
        {
            return View();
        }
    }
}
