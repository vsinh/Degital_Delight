using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Services.Interfaces;
using Hangfire;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace DegitalDelight.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        private readonly ApplicationDbContext _context;

        public OrderController(ILogger<OrderController> logger, ApplicationDbContext context, IOrderService orderService)
        {
            _logger = logger;
            _context = context;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            // Lấy danh sách sản phẩm từ cơ sở dữ liệu
            //var products = _context.Products.ToList();


            if (!_context.Products.Any())
            {
                // Thêm dữ liệu mẫu
                _context.Products.AddRange(
                    new Product { Name = "Sản phẩm 1", Price = 100, Picture = "https://topthuthuat.com/wp/wp-content/uploads/2020/05/ban-phim-laptop-bi-loi.jpg", Description = "Mô tả sản phẩm 1" },
                    new Product { Name = "Sản phẩm 2", Price = 150, Picture = "https://external-preview.redd.it/K0tk-IHUnEfLqJPjioA3zTCtlW-SPfz8FEDwpnyc7JE.jpg?auto=webp&s=2e35a9c7d62acc359f81231fd7b84010bee2e06b", Description = "Mô tả sản phẩm 2" },
                    new Product { Name = "Sản phẩm 3", Price = 200, Picture = "https://th.bing.com/th/id/OIP.ANiizuZQscV6Xd1xkw35FQHaE7?w=626&h=417&rs=1&pid=ImgDetMain", Description = "Mô tả sản phẩm 3" },
                    new Product { Name = "Sản phẩm 4", Price = 120, Picture = "laptop.jpg", Description = "Mô tả sản phẩm 4" }
                );

                _context.SaveChanges();
            }

            // Lấy danh sách sản phẩm từ cơ sở dữ liệu
            var products = _context.Products.ToList();

            // Truyền danh sách sản phẩm đến view
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            Order order = new Order();
            await _orderService.CreateOrder(order);
            return RedirectToAction("Index");
        }
    }
}
