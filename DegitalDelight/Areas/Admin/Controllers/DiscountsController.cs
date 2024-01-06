﻿using System;
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
        private readonly ApplicationDbContext _context;
        private readonly IDiscount _discountService;

        public DiscountsController(ApplicationDbContext context, IDiscount discountService)
        {
            _context = context;
            _discountService = discountService;
        }

        // GET: Admin/Discounts
        public async Task<IActionResult> Index()
        {
			return View(await _discountService.GetAllDiscounts());
		}

        // GET: Admin/Discounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Discounts == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

		// GET: Admin/Discounts/Create
		public IActionResult CreateCode()
		{
			return View();
		}

		// POST: Admin/Discounts/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Type,Amount,StartDate,EndDate,IsDeleted")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discount);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateCode([Bind("Type,StartDate,EndDate,Code,Quantity,Amount")] Discount discount)
		{
            if (discount.EndDate < discount.StartDate)
                return View(discount);
			if (ModelState.IsValid)
			{
				await _discountService.CreateDisount(discount);
				return RedirectToAction("Index", "Discounts");
			}
			return View(discount);
		}

		// GET: Admin/Discounts/Edit/5
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Discounts == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }
            return View(discount);
        }

        // POST: Admin/Discounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Type,Amount,StartDate,EndDate,IsDeleted")] Discount discount)
        {
            if (id != discount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountExists(discount.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(discount);
        }

        // GET: Admin/Discounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Discounts == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // POST: Admin/Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Discounts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Discounts'  is null.");
            }
            var discount = await _context.Discounts.FindAsync(id);
            if (discount != null)
            {
                _context.Discounts.Remove(discount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountExists(int id)
        {
          return (_context.Discounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
