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
		public async Task<IActionResult> AddItemToFavorite(int id)
		{
			var result = await _favoriteService.AddItemToFavorite(id);
			if (result)
			{
                return RedirectToAction("Homepage", "Home");
            }
			else
			{
				return NotFound();
			}
		}

		[Authorize]
		public async Task<IActionResult> RemoveItemFromFavorite(int id)
		{
			var result = await _favoriteService.RemoveItemFromFavorite(id);

			if (result)
			{
                return Json(200);
            }

			return NotFound();
		}

	}
}
