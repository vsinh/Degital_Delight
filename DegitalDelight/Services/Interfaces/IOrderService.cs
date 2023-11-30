using DegitalDelight.Models;

namespace DegitalDelight.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);
        void SendEmailConfirmOrder(int orderId);
    }
}
