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
	public class WarrantyController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IWarranty _warrantyService;

		public WarrantyController(ApplicationDbContext context, IWarranty warrantyService)
		{
			_context = context;
			_warrantyService = warrantyService;
		}

		public async Task<ActionResult> Index()
		{
			return View(await _warrantyService.GetAllWarranties());
		}
		public IActionResult Create()
		{
			return View();
		}
		public async Task<ActionResult> Edit(int id)
		{
			return View(await _warrantyService.GetWarranty(id));
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,Duration,Description")] Warranty warranty)
		{
			if (ModelState.IsValid)
			{
				if (warranty.Duration <= 0 )
				{
					return View(warranty);
				}
				await _warrantyService.CreateWarranty(warranty);
				return RedirectToAction("Index", "Warranty");
			}
			return View(warranty);
		}

		[HttpGet]
		public async Task<IActionResult> Search(string input)
		{
			var warranties = await _warrantyService.SearchWaranties(input);
			return Json(warranties);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([Bind("Id, Name, Description, Duration")] Warranty warranty)
		{
			if (ModelState.IsValid)
			{
				await _warrantyService.EditWarranty(warranty);
				return RedirectToAction("Index", "Warranty");
			}
			return View(warranty);
		}
	}
}
