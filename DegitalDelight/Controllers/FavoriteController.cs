using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Services;
using Hangfire;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using DegitalDelight.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DegitalDelight.Controllers
{
	public class FavoriteController : Controller
	{
		private readonly IFavoriteService _favoriteService;
		public FavoriteController(IFavoriteService favoriteService)
		{
			_favoriteService = favoriteService;
		}
		public ActionResult Favorite()
		{
			return View();
		}

		public async Task<IActionResult> Index()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (userId != null)
			{
				var favoriteItems = await _favoriteService.GetFavoriteProducts(userId);
				return View(favoriteItems);
			}
			else
			{
				return RedirectToAction("Error");
			}
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> AddItemToFavorite(int productId)
		{
			var result = await _favoriteService.AddItemToFavorite(productId);
			if (result)
			{
                return RedirectToAction("Homepage", "Home");
            }
			else
			{
				return NotFound();
			}
		}
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> RemoveItemFromFavorite(int productId, string userId)
		{
			var result = await _favoriteService.RemoveItemFromFavorite(productId,userId);

			if (result)
			{
                return RedirectToAction("Homepage", "Home");
            }

			return NotFound();
		}

	}
}
