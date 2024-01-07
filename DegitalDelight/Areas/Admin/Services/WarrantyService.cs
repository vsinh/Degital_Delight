using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Data;
using DegitalDelight.Models;
using Microsoft.EntityFrameworkCore;

namespace DegitalDelight.Areas.Admin.Services
{
	public class WarrantyService : IWarranty
	{
		private readonly ApplicationDbContext _context;
		public WarrantyService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task CreateWarranty(Warranty warranty)
		{
			await _context.AddAsync(warranty);
			await _context.SaveChangesAsync();
		}

		public async Task EditWarranty(Warranty warranty)
		{
			_context.Update(warranty);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Warranty>> GetAllWarranties()
		{
			return await _context.Warrantys.ToListAsync();
		}

		public async Task<Warranty> GetWarranty(int id)
		{
			return await _context.Warrantys.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<Warranty>> SearchWaranties(string input)
		{
			if (string.IsNullOrEmpty(input))
				return await _context.Warrantys.ToListAsync();
			var warranties = await _context.Warrantys.Where(x =>
				((x.Name).Contains(input)
				|| (x.Duration.ToString()).Contains(input))
			).ToListAsync();
			return warranties;
		}
	}
}
