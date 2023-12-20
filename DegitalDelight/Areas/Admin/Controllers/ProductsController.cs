using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Models.DTO;

namespace DegitalDelight.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProduct _productService;

        public ProductsController(ApplicationDbContext context, IProduct productService)
        {
            _context = context;
            _productService = productService;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllProducts());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProducts(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Picture,Description,IsDeleted")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProducts(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Picture,Description")] ProductDTO product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _productService.EditProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            await _productService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
