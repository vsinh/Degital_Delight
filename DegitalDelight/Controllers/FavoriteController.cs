using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Services;
using DegitalDelight.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddItemToFavorite(int productId)
		{
			var result = await _favoriteService.AddItemToFavorite(productId);
			if (result)
			{
				return Ok();
			}
			else
			{
				return NotFound();
			}
		}
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> RemoveItemFromFavorite(int productId)
		{
			var result = await _favoriteService.RemoveItemFromFavorite(productId);

			if (result)
			{
				return Ok();
			}

			return NotFound();
		}

	}
}
