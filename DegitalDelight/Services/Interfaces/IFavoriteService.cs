using DegitalDelight.Models;

namespace DegitalDelight.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task<bool> AddItemToFavorite(int productId);
        Task<List<Favorite>> GetFavoriteProducts(string userId);
        Task<bool> RemoveItemFromFavorite(int productId, string userId);
    }
}
