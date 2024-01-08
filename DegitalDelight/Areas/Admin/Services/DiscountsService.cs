using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Data;
using DegitalDelight.Models;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace DegitalDelight.Areas.Admin.Services
{
	public class DiscountsService : IDiscount
	{
		private readonly ApplicationDbContext _context;
		public DiscountsService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task CreateDiscount(Discount discount)
		{
			if (discount.StartDate > DateTime.Now)
			{
				discount.IsDeleted = true;
				BackgroundJob.Schedule(() => StartDiscount(discount), discount.StartDate);
			}
			if (discount.EndDate < DateTime.Now)
			{
				BackgroundJob.Schedule(() => EndDiscount(discount), discount.EndDate);
			}
			await _context.AddAsync(discount);
			await _context.SaveChangesAsync();
		}

		public async Task CreateProductDiscount(Discount discount)
		{
			Product product = _context.Products.FirstOrDefault(x => x.Id == discount.Product.Id);
			discount.Product = product;

			if (discount.StartDate > DateTime.Now)
			{
                discount.IsDeleted = true;
				BackgroundJob.Schedule(() => StartDiscount(discount), discount.StartDate);
            }
			if (discount.EndDate < DateTime.Now)
			{
				BackgroundJob.Schedule(() => EndDiscount(discount), discount.EndDate);
			}

			await _context.AddAsync(discount);
			await _context.SaveChangesAsync();
		}

		public void StartDiscount(Discount discount)
		{
			discount.IsDeleted = false;
			_context.Update(discount);
			_context.SaveChanges();
		}

		public void EndDiscount(Discount discount)
        {
            discount.IsDeleted = true;
            _context.Update(discount);
            _context.SaveChanges();
        }

		public Task DeleteDiscount(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Discount>> GetAllDiscounts()
		{
			return await _context.Discounts.Include(x => x.Product).Where(x => !x.IsDeleted).ToListAsync();
		}

		public async Task<Discount> GetDiscountById(int id)
		{
			return await _context.Discounts.Include(x => x.Product).Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<Discount>> SearchProductDiscounts(string input)
		{
			if (string.IsNullOrEmpty(input))
				return await _context.Discounts.Where(x => !x.IsDeleted && x.Type == "Product").Include(x => x.Product).ToListAsync();
			var discountList = await _context.Discounts.Include(x => x.Product).Where(x =>
			(x.Product.Name.Contains(input)
			|| x.StartDate.ToString().Contains(input)
			|| x.EndDate.ToString().Contains(input)) && !x.IsDeleted && x.Type == "Product"
			).ToListAsync();
			return discountList;
		}
		public async Task<List<Discount>> SearchCodeDiscounts(string input)
		{
			if (string.IsNullOrEmpty(input))
				return await _context.Discounts.Where(x => !x.IsDeleted && x.Type == "code").ToListAsync();
			var discountList = await _context.Discounts.Where(x =>
			(x.Code.Contains(input)
			|| x.StartDate.ToString().Contains(input)
			|| x.EndDate.ToString().Contains(input)) && !x.IsDeleted && x.Type == "code"
			).ToListAsync();
			return discountList;
		}
		public async Task EditDiscount(Discount discount)
		{
			_context.Update(discount);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Discount>> GetAllDiscountExceptThis(Discount discount)
		{
			return await _context.Discounts.Where(x => !x.Equals(discount)).ToListAsync();
		}

		public async Task<bool> CheckExistCode(Discount discount)
		{
			var discounts = await GetAllDiscountExceptThis(discount);
			foreach (var item in discounts)
			{
				if (discount.Code == item.Code)
				{
					return true;
				}
			}
			return false;
		}
		public async Task DeleteDiscount(Discount discount)
		{
			discount.IsDeleted = true;
			await _context.SaveChangesAsync();
		}
	}
}
