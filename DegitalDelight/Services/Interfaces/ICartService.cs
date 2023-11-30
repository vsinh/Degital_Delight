using DegitalDelight.Models;

namespace DegitalDelight.Services.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddItemToCart(int productId);
        void RemindOrder(string userName);
    }
}
