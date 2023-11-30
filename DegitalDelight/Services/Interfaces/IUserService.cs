using DegitalDelight.Models;

namespace DegitalDelight.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetCurrentUser();
    }
}
