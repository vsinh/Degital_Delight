using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace DegitalDelight.Areas.Admin.Services
{
    public class ProductService : IProduct
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
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
            var oldProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            if (oldProduct != null)
            {
                oldProduct.Name = product.Name;
                oldProduct.Description = product.Description;
                oldProduct.ProductType = _context.ProductTypes.FirstOrDefault(x => x.Id == product.ProductTypeId);
                oldProduct.Price = product.Price;
                oldProduct.Picture = product.Picture;
                await _context.SaveChangesAsync();

            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProducts(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> SearchProducts(string name, int productTypeId = -1)
        {
            var productList = await _context.Products.Include(x => x.ProductType).Where(x => x.Name.Contains(name)).ToListAsync();
            if (productTypeId != -1)
            {
                productList = productList.Where(x => x.ProductType.Id == productTypeId).ToList();
            }
            return productList;
        }
    }
}
