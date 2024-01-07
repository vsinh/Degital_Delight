using DegitalDelight.Models;

namespace DegitalDelight.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetCurrentUser();
        Task<List<User>> GetUserList();
        Task<User> GetUserById(string id);
        Task<bool> IsAdmin(string id);
        Task<bool> EditUser(User user);
        Task<bool> EditUserRole(User user, int role);
        Task<bool> RevertState(string id);
    }
}
