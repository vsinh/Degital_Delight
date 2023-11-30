using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Services.Interfaces;

namespace DegitalDelight.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly ApplicationDbContext _context;
        public DiscountService(ApplicationDbContext context)
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
