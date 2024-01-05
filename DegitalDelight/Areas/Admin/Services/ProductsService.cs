using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace DegitalDelight.Areas.Admin.Services
{
	public class ProductsService : IProduct
	{
		private readonly ApplicationDbContext _context;
		public ProductsService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task CreateProduct(Product product)
		{
			await _context.Products.AddAsync(product);
		}

		public async Task DeleteProduct(int id)
		{
			var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
			if (product != null)
			{
				product.IsDeleted = true;
				await _context.SaveChangesAsync();
			}
		}

		public async Task EditProduct(ProductDTO product)
		{
			var oldSupply = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
			if (oldSupply != null)
			{
				oldSupply.Name = product.Name;
				oldSupply.Description = product.Description;
				oldSupply.ProductType = _context.ProductTypes.FirstOrDefault(x => x.Id == product.ProductTypeId);
				oldSupply.Price = product.Price;
				oldSupply.Picture = product.Picture;
				await _context.SaveChangesAsync();

			}
		}

		public async Task<List<Product>> GetAllProducts()
		{
			return await _context.Products.Include(x => x.ProductType).ToListAsync();
		}

		public async Task<Product> GetProducts(int id)
		{
			return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<Product>> SearchProducts(string input)
		{
			if (string.IsNullOrEmpty(input))
				return await _context.Products.Include(x => x.ProductType).ToListAsync();
			var productList = await _context.Products.Include(x => x.ProductType).Where(x =>
			x.Name.Contains(input)
			|| x.ProductType.Name.Contains(input)
			|| x.Brand.Contains(input)
			).ToListAsync();
			return productList;
		}
	}
}
