using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Areas.Admin.Services;
using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Models.DTO;

namespace DegitalDelight.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiscountsController : Controller
    {
        private readonly IDiscount _discountService;
        private readonly IProduct _productService;

        public DiscountsController(IDiscount discountService, IProduct productService)
        {
            _discountService = discountService;
            _productService = productService;
        }

        // GET: Admin/Discounts
        public async Task<IActionResult> Index()
        {
			return View(await _discountService.GetAllDiscounts());
		}

		public async Task<IActionResult> SearchProductType(string input)
		{
			var discounts = await _discountService.SearchProductDiscounts(input);
			return Json(discounts);
		}
		public async Task<IActionResult> SearchCode(string input)
		{
			var discounts = await _discountService.SearchCodeDiscounts(input);
			return Json(discounts);
		}

		// GET: Admin/Discounts/Create
		public IActionResult CreateCodeDiscount()
		{
			return View();
		}

		public async Task<IActionResult> CreateProductDiscount(int id)
		{
            ViewData["Product"] = await _productService.GetProducts(id);
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateCodeDiscount([Bind("Type,StartDate,EndDate,Code,Quantity,Amount")] Discount discount)
		{
            if (discount.EndDate < discount.StartDate)
                return View(discount);
            var discounts = await _discountService.GetAllDiscounts();
            foreach(var item in discounts)
            {
                if (discount.Code == item.Code)
                    return View(discount);
            }
			if (ModelState.IsValid)
			{
				await _discountService.CreateDiscount(discount);
				return RedirectToAction("Index", "Discounts");
			}
			return View(discount);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateProductDiscount([Bind("Type,StartDate,EndDate,Quantity,Amount")] Discount discount, int ProductId)
		{
			if (discount.EndDate < discount.StartDate)
				return View(discount);
			if (ModelState.IsValid)
			{
                Product newproduct = new Product();
                newproduct.Id = ProductId;
                discount.Product = newproduct;
				await _discountService.CreateProductDiscount(discount);
				return RedirectToAction("Index", "Discounts");
			}
			ViewData["Product"] = await _productService.GetProducts(ProductId);
			return View(discount);
		}

		public async Task<IActionResult> EditProductDiscount(int id)
        {
            var discount = await _discountService.GetDiscountById(id);
            if (discount == null)
            {
                return NotFound();
            }
            return View(discount);
        }
		public async Task<IActionResult> EditCodeDiscount(int id)
		{
			var discount = await _discountService.GetDiscountById(id);
			if (discount == null)
			{
				return NotFound();
			}
			return View(discount);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditCodeDiscount([Bind("Id,Type,StartDate,EndDate,Code,Quantity,Amount")] Discount discount, int productId)
		{
			if (discount.EndDate < discount.StartDate)
				return View(discount);
			if (await _discountService.CheckExistCode(discount))
			{
				return View(discount);
			}
			if (ModelState.IsValid)
			{
				await _discountService.EditDiscount(discount);
				return RedirectToAction("Index", "Discounts");
			}
			var oldDiscount = await _discountService.GetDiscountById(discount.Id);
			discount.Product = oldDiscount.Product;
			return View(oldDiscount);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditProductDiscount([Bind("Id,Type,StartDate,EndDate,Quantity,Amount")] Discount discount)
		{
			if (discount.EndDate < discount.StartDate)
				return View(discount);
			if (ModelState.IsValid)
			{
				await _discountService.EditDiscount(discount);
				return RedirectToAction("Index", "Discounts");
            }
            var oldDiscount = await _discountService.GetDiscountById(discount.Id);
            discount.Product = oldDiscount.Product;
            return View(discount);
		}
	}
}
