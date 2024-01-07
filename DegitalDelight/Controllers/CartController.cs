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
using System.Security.Claims;
using Newtonsoft.Json.Linq;

namespace DegitalDelight.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public IActionResult Cart()
        {
            return View();
        }

        public async Task<IActionResult> Index()
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

        [HttpPost]
        public async Task<IActionResult> AddItemToCart(int productId)
        {
            var result = await _cartService.AddItemToCart(productId);
            if (result)
            {
                return RedirectToAction("Homepage", "Home");
            }
            else
            {
                return NotFound();
            }
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
        [HttpPost]
        public async Task<IActionResult> RemoveItemFromCart(int productId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                var result = await _cartService.RemoveItemFromCart(productId, userId);

                if (result)
                {
                    return RedirectToAction("Homepage", "Home");
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> RemoveItemFromCart2(int productId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                var result = await _cartService.RemoveItemFromCart(productId, userId);

                if (result)
                {
                    return RedirectToAction("Cart", "Cart");
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCartItemAmount(int productId, int newAmount)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                var result = await _cartService.UpdateCartItemAmount(productId, userId, newAmount);

                if (result)
                {
                    return RedirectToAction("ViewCart", "Cart");
                }
            }

            return NotFound();
        }   
    }
}

