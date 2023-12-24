using DegitalDelight.Models;
using DegitalDelight.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace DegitalDelight.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Product(int productId)
        {
            var product = GetProductById(productId);

            // Pass the product details to the CartView
            return View(product);
        }


        private Product GetProductById(int productId)
        {
            // Replace this with your actual logic to fetch product details from the database or any other source
            // For demonstration purposes, a sample Product class is assumed here
            return new Product
            {
                Id = productId,
                Name = "Laptop thông minh",
                Price = 100,
                Description = "Laptop thông minh.",
                Picture = "https://topthuthuat.com/wp/wp-content/uploads/2020/05/ban-phim-laptop-bi-loi.jpg"
            };
        }
    }
}