using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DegitalDelight.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<Product> GetProductById(int productId)
        {
            return await _context.Products.Where(x => x.Id == productId && !x.IsDeleted).FirstOrDefaultAsync();
        }

        public Task<Product> GetProductById(List<int> productIds)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.Where(x => !x.IsDeleted).ToListAsync();
        }
        public async Task<List<Product>> GetProductList(int productTypeId, int maxPrice, int minPrice, string sort)
        {
            return await _context.Products.Include(x => x.ProductType)
                .Where(x => 
                x.ProductType.Id == productTypeId
                && x.Price >= minPrice 
                && x.Price <= maxPrice 
                && !x.IsDeleted
            ).ToListAsync();
        }

        public Task<List<Product>> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Include(x => x.ProductType).Where(x => x.ProductType.Id == categoryId).ToListAsync();
        }

        public async Task<List<ProductType>> GetProductTypes()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}
