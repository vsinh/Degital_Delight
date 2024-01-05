using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace DegitalDelight.Areas.Admin.Services
{
	public class SupplyService : ISupply
	{
		private readonly ApplicationDbContext _context;
		public SupplyService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task CreateSupplies(Supply supply)
		{
			await _context.Supplies.AddAsync(supply);
		}

		public async Task DeleteSupply(int id)
		{
			var supply = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
			if (supply != null)
			{
				supply.IsDeleted = true;
				await _context.SaveChangesAsync();
			}
		}

		public async Task EditSupply(SupplyDTO supply)
		{
			var oldSupply = await _context.Supplies.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == supply.Id);
			if (oldSupply != null)
			{
				var oldProduct = oldSupply.Product;
				if (oldProduct != null)
				{
					oldProduct.Stock -= oldSupply.Amount;
				}

				oldSupply.Price = supply.Price;
				oldSupply.Amount = supply.Amount;
				oldSupply.Date = supply.Date;
				oldSupply.Product = _context.Products.FirstOrDefault(x => x.Id == supply.ProductId);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<Supply>> GetAllSupplies()
		{
			return await _context.Supplies.ToListAsync();
		}

		public async Task<Supply> GetSupplies(int id)
		{
			return await _context.Supplies.FindAsync(id);
		}

		public async Task<List<Supply>> SearchSupplies(string input)
		{
			if (string.IsNullOrEmpty(input))
				return await _context.Supplies.Include(x => x.Product).ToListAsync();
			var supplies = await _context.Supplies.Include(x => x.Product).Where(x =>
				(x.Id.ToString()).Contains(input)
				|| (x.Product.Name).Contains(input)
				|| (x.Product.Id.ToString()).Contains(input)
			).ToListAsync();
			return supplies;
		}
	}
}
