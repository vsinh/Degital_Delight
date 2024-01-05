using DegitalDelight.Models;

namespace DegitalDelight.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductById(int productId);
        Task<List<Product>> GetProducts();
        Task<List<Product>> GetProductsByCategory(int categoryId);
        Task<List<ProductType>> GetProductTypes();
    }
}
