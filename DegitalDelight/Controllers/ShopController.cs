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
        public async Task<IActionResult> Shop(int id = 1)
        {
            ViewData["ProductTypes"] = await _productService.GetProductTypes();
            return View(await _productService.GetProducts());
        }
    }
}
