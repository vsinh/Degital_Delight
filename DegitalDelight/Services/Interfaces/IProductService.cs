using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Models;

namespace DegitalDelight.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductById(int productId);
        Task<Product> GetProductById(List<int> productIds);
        Task<List<Product>> GetProducts();
        Task<List<Product>> GetProductsByCategory(int categoryId);
        Task<List<ProductType>> GetProductTypes();
        Task<List<Product>> GetProductList(int productTypeId, int maxPrice, int minPrice, string sort);
        Task<List<Product>> GetRelatedProduct(Product product);
        Task<int> GetProductDiscount(int productId);
        Task<int> GetProductPriceAfterDiscount(int productId);
        Task<List<Product>> Search(string input);
        Task<List<Product>> ExploreProducts();
        Task<List<Product>> LatestProduct();
        Task<List<Product>> GetLargestStockProduct();
        Task<List<Product>> GetProductWithLargestDiscount();


    }
}
