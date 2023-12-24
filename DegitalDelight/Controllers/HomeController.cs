using DegitalDelight.Data;
using DegitalDelight.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace DegitalDelight.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Homepage()
        {
            // Lấy danh sách sản phẩm từ cơ sở dữ liệu
            //var products = _context.Products.ToList();

     
            if (!_context.Products.Any())
            {
                // Thêm dữ liệu mẫu
                _context.Products.AddRange(
                    new Product { Name = "Sản phẩm 1", Price = 100,Picture = "https://topthuthuat.com/wp/wp-content/uploads/2020/05/ban-phim-laptop-bi-loi.jpg",   Description = "Mô tả sản phẩm 1" },
                    new Product { Name = "Sản phẩm 2", Price = 150,Picture = "https://external-preview.redd.it/K0tk-IHUnEfLqJPjioA3zTCtlW-SPfz8FEDwpnyc7JE.jpg?auto=webp&s=2e35a9c7d62acc359f81231fd7b84010bee2e06b", Description = "Mô tả sản phẩm 2" },
                    new Product { Name = "Sản phẩm 3", Price = 200,Picture = "https://th.bing.com/th/id/OIP.ANiizuZQscV6Xd1xkw35FQHaE7?w=626&h=417&rs=1&pid=ImgDetMain", Description = "Mô tả sản phẩm 3" },
                    new Product { Name = "Sản phẩm 4", Price = 120,Picture = "laptop.jpg", Description = "Mô tả sản phẩm 4" }
                );

                _context.SaveChanges();
            }

            // Lấy danh sách sản phẩm từ cơ sở dữ liệu
            var products = _context.Products.ToList();

            // Truyền danh sách sản phẩm đến view
            return View(products);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult myAccount()
        {
            return View();
        }

        public IActionResult Order()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
