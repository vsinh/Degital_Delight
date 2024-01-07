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
using DegitalDelight.Areas.Admin.Services;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using DegitalDelight.Data.Migrations;
using DegitalDelight.Services.Interfaces;

namespace DegitalDelight.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Administrator")]
	public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProduct _productService;
        private readonly IPictureService _pictureService;

        public ProductsController(ApplicationDbContext context, IProduct productService, IPictureService pictureService)
        {
            _context = context;
            _productService = productService;
            _pictureService = pictureService;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            TempData["ProductTypes"] = await _productService.GetAllProductTypes();
            return View(await _productService.GetAllProducts());
        }

		[HttpGet]
		public async Task<IActionResult> Search(string input)
		{
			var products = await _productService.SearchProducts(input);
			return Json(products);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
            await _productService.DeleteProduct(id);
            return RedirectToAction("Index");
		}

        [HttpGet]
        public async Task<IActionResult> DeleteProductType(int id)
        {
            await _productService.DeleteProductType(id);
            return RedirectToAction("Index");
        }

        // GET: Admin/Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProductTypes"] = new SelectList(await _productService.GetAllProductTypes(), "Id", "Name");
            return View();
        }
		public async Task<IActionResult> CreateProductType()
		{
            return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProductType([Bind("Name,Description")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Products");
            }
            return View(productType);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,ProductTypeId,Description,Brand")] ProductDTO product, IFormCollection form, IFormFile fileInput)
        {
            if (ModelState.IsValid)
            {
                List<String> namevalue = new List<String>();
                foreach (var item in form.Keys)
                {
                    if (item.Contains('-'))
                    {
                        var nameid = item.Split('-')[1];
                        var value = form[item];
                        namevalue.Add(value);
                    }
                }

                if (fileInput != null)
                {
                    product.Picture = _pictureService.CreateProductPicture(fileInput);
                }
                else
                {
                    product.Picture = "product-placeholder.png";
                }

                List<ProductDetail> productDetails = new List<ProductDetail>();

				for (int i = 0; i < namevalue.Count; i+=2)
                {
                    var productDetail = new ProductDetail();
                    productDetail.Property = namevalue[i];
                    productDetail.Value = namevalue[i+1];
                    productDetails.Add(productDetail);
                }
				await _productService.CreateProduct(product, productDetails);
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
			ProductDTO productDTO = new ProductDTO();
			productDTO.Id = id;
			productDTO.Name = product.Name;
			productDTO.ProductTypeId = product.ProductType.Id;
			productDTO.Description = product.Description;
			productDTO.Price = product.Price;
			productDTO.Picture = product.Picture;
			productDTO.Brand = product.Brand;
			foreach (var item in product.ProductDetails)
			{
				productDTO.ProductDetails.Add(item);
			}
			ViewData["ProductTypes"] = new SelectList(await _productService.GetAllProductTypes(), "Id", "Name", product.ProductType.Id);
            return View(productDTO);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Price,ProductTypeId,Description,Brand")] ProductDTO product, IFormCollection form, IFormFile fileInput)
        {
			if (ModelState.IsValid)
			{
				List<String> namevalue = new List<String>();
				foreach (var item in form.Keys)
				{
					if (item.Contains('-'))
					{
						var value = form[item];
						namevalue.Add(value);
					}
				}

				if (fileInput != null)
				{
					product.Picture = _pictureService.CreateProductPicture(fileInput);
				}
				else
				{
					product.Picture = "product-placeholder.png";
				}

				List<ProductDetail> productDetails = new List<ProductDetail>();

				for (int i = 0; i < namevalue.Count; i += 2)
				{
					var productDetail = new ProductDetail();
					productDetail.Property = namevalue[i];
					productDetail.Value = namevalue[i + 1];
					productDetails.Add(productDetail);
				}
				await _productService.EditProduct(product, productDetails);
				return RedirectToAction("Index");
			}
			return View(product);
		}
    }
}
