﻿using DegitalDelight.Models;
using DegitalDelight.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace DegitalDelight.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Product()
        {
            return View();
        }
        public async Task<IActionResult> Product(int productId)
        {
            var product = await _productService.GetProductById(productId);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> Products()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }

        public async Task<IActionResult> ProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategory(categoryId);
            return View(products);
        }

        public async Task<IActionResult> ProductTypes()
        {
            var productTypes = await _productService.GetProductTypes();
            return View(productTypes);
        }
    }
}