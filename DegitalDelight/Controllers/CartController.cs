using DegitalDelight.Data;
using DegitalDelight.Models;
using Hangfire;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using DegitalDelight.Services.Interfaces;

namespace DegitalDelight.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public ActionResult Index(int productId)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCart(int productId)
        {
            var result = await _cartService.AddItemToCart(productId);
            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
