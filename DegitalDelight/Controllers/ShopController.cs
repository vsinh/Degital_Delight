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
        public async Task<IActionResult> Shop(int id = 1, int minPrice = 0, int maxPrice = int.MaxValue, string sort = "latest")
        {
            ViewData["ProductTypes"] = await _productService.GetProductTypes();
            ViewData["CurrentId"] = id;
            return View(await _productService.GetProductList(id, minPrice,maxPrice, sort));
        }
    }
}
