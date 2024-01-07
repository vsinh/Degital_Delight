using DegitalDelight.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DegitalDelight.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        public ShopController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Shop(int id = 1, int minPrice = 0, int maxPrice = 40000000, string sort = "Latest")
        {
            ViewData["ProductTypes"] = await _productService.GetProductTypes();
            ViewData["CurrentId"] = id;
            ViewData["CurrentMinPrice"] = minPrice;
            ViewData["CurrentMaxPrice"] = maxPrice;
            ViewData["CurrentSort"] = maxPrice;
            return View(await _productService.GetProductList(id, minPrice,maxPrice, sort));
        }
    }
}
