using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Data;
using DegitalDelight.Models;
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
		public async Task CreateDisount(Discount discount)
		{
			await _context.AddAsync(discount);
			await _context.SaveChangesAsync();
		}

		public Task DeleteDiscount(int id)
		{
			throw new NotImplementedException();
		}

		public Task EditDiscount(Discount supply)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Discount>> GetAllDiscounts()
		{
			return await _context.Discounts.Include(x => x.Product).Where(x => !x.IsDeleted).ToListAsync();
		}

		public Task<Discount> GetDiscountById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<Discount>> SearchDiscounts(string input)
		{
			throw new NotImplementedException();
		}
	}
}
