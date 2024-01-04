using DegitalDelight.Models;
using DegitalDelight.Models.DTO;

namespace DegitalDelight.Areas.Admin.Services.Interfaces
{
    public interface IProduct
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProducts(int id);
        Task<List<Product>> SearchProducts(string input);
        Task CreateProduct(Product product);
        Task EditProduct(ProductDTO product);
        Task DeleteProduct(int id);
    }
}
