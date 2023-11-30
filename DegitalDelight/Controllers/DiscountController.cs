using DegitalDelight.Data;
using DegitalDelight.Models;
using Microsoft.AspNetCore.Mvc;

namespace DegitalDelight.Controllers
{
    public class DiscountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DiscountController(ApplicationDbContext context)
        {
            _context = context;
        }
        public void SundayDiscount()
        {
            var productList = _context.Products.ToList();
            foreach (var product in productList)
            {
                Discount discount = new Discount();
                discount.Type = "percent";
                discount.Amount = 20;
                discount.Product = product;
                discount.StartDate = DateTime.Now;
                discount.EndDate = DateTime.Now + TimeSpan.FromHours(24);
            }
            
        }
    }
}
