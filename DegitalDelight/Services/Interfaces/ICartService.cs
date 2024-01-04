using DegitalDelight.Models;

namespace DegitalDelight.Services.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddItemToCart(int productId);
        void RemindOrder(string userName);
        Task<List<CartItem>> GetCartItems(string userId);
        Task<bool> RemoveItemFromCart(int productId, string userId);
    }
}
