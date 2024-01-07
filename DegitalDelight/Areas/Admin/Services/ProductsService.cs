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
		public async Task CreateProduct(ProductDTO product, List<ProductDetail> productDetails)
		{
			Product newproduct = new Product();
			newproduct.Name = product.Name;
			newproduct.ProductType = _context.ProductTypes.FirstOrDefault(x => x.Id==product.ProductTypeId);
			newproduct.Warranty = _context.Warrantys.FirstOrDefault(x => x.Id == product.WarrantyId);
			newproduct.Description = product.Description;
			newproduct.Price = product.Price;
			newproduct.Picture = product.Picture;
			newproduct.Brand = product.Brand;
			newproduct.CreatedDate = DateTime.Now;
			await _context.Products.AddAsync(newproduct);
			await _context.SaveChangesAsync();
			foreach (var item in productDetails)
			{
				item.ProductId = newproduct.Id;
				await _context.ProductDetails.AddAsync(item);
			}
			await _context.SaveChangesAsync();
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

		public async Task EditProduct(ProductDTO product, List<ProductDetail> productDetails)
		{
			var oldProduct = await _context.Products.Include(x => x.ProductDetails).Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Id == product.Id);
			if (oldProduct != null)
			{
				oldProduct.Name = product.Name;
				oldProduct.Description = product.Description;
				oldProduct.ProductType = _context.ProductTypes.FirstOrDefault(x => x.Id == product.ProductTypeId);
				oldProduct.Warranty = _context.Warrantys.FirstOrDefault(x => x.Id == product.WarrantyId);
				oldProduct.Price = product.Price;
				oldProduct.Picture = product.Picture;
				oldProduct.Brand = product.Brand;

				foreach (var item in oldProduct.ProductDetails)
				{
					_context.ProductDetails.Remove(item);
				}
				foreach (var item in productDetails)
				{
					item.ProductId = oldProduct.Id;
					_context.ProductDetails.Add(item);
				}
				await _context.SaveChangesAsync();

			}
		}

		public async Task<List<Product>> GetAllProducts()
		{
			return await _context.Products.Include(x => x.ProductType).Where(x => !x.IsDeleted && !x.ProductType.IsDeleted).ToListAsync();
		}
        public async Task<List<ProductType>> GetAllProductTypes()
        {
            return await _context.ProductTypes.Where(x => !x.IsDeleted).ToListAsync();
        }
        public async Task<Product> GetProducts(int id)
		{
			return await _context.Products.Include(x => x.ProductType).Include(x => x.Warranty).Include(x => x.ProductDetails).Where(x => !x.IsDeleted && !x.ProductType.IsDeleted).FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<Product>> SearchProducts(string input)
		{
			if (string.IsNullOrEmpty(input))
				return await _context.Products.Include(x => x.ProductType).Where(x => !x.IsDeleted && !x.ProductType.IsDeleted).ToListAsync();
			var productList = await _context.Products.Include(x => x.ProductType).Where(x =>
			(x.Name.Contains(input)
			|| x.ProductType.Name.Contains(input)
			|| x.Brand.Contains(input)
			) && !x.IsDeleted && !x.ProductType.IsDeleted
			).ToListAsync();
			return productList;
		}
        public async Task DeleteProductType(int id)
        {
            var productType = await _context.ProductTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (productType != null)
            {
                productType.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
