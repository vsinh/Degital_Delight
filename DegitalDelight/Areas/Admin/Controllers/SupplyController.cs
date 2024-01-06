using DegitalDelight.Areas.Admin.Services;
using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DegitalDelight.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Administrator")]
	public class SupplyController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ISupply _supplyService;

		public SupplyController(ApplicationDbContext context, ISupply suppplyService)
		{
			_context = context;
			_supplyService = suppplyService;
		}
		// GET: SupplyController
		public async Task<ActionResult> Supply()
		{
			return View(await _supplyService.GetAllSupplies());
		}

		// GET: SupplyController/Create
		public async Task<IActionResult> Create(int id)
		{
			ViewData["Product"] = await _supplyService.GetProductById(id);
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Amount,Price,ProductId")] SupplyDTO supply, IFormCollection form)
		{
			if (ModelState.IsValid)
			{
				await _supplyService.CreateSupplies(supply);
				return RedirectToAction("Index","Products");
			}
			return View(supply);
		}

		[HttpGet]
		public async Task<IActionResult> Search(string input)
		{
			var supplies = await _supplyService.SearchSupplies(input);
			return Json(supplies);
		}

		// GET: SupplyController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: SupplyController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// POST: SupplyController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
