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

        public IActionResult Order()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            Order order = new Order();
            await _orderService.CreateOrder(order);
            return RedirectToAction("Homepage","Home");
        }
    }
}
