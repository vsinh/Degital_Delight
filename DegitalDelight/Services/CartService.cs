using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Services.Interfaces;
using Hangfire;
using MailKit.Security;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using System.Security.Claims;

namespace DegitalDelight.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public CartService(ApplicationDbContext applicationDbContext, 
            IEmailService emailService,
            IUserService userService)
        {
            _context = applicationDbContext;
            _emailService = emailService;
            _userService = userService;
        }
        public async Task<bool> AddItemToCart(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                var cartItem = new CartItem();
                cartItem.Product = product;

                var currentUser = await _userService.GetCurrentUser();

                cartItem.User = currentUser;
                cartItem.Amount = 1;

                BackgroundJob.Schedule(() => RemindOrder(currentUser.UserName), TimeSpan.FromHours(1));

                return true;
            }
            return false;
        }

        public void RemindOrder(string userName)
        {
            _emailService.SendMail("sorunaito@gmail.com", "Bạn còn món hàng trong giỏ", "Bấm đặt hàng ngay");
        }
    }
}
