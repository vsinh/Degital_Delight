using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DegitalDelight.Services
{
    public class UserService : IUserService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        public UserService(ApplicationDbContext applicationDbContext,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService)
        {
            _context = applicationDbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
        }
        public async Task<User> GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var currentUser = await _userManager.GetUserAsync(user);
            if (currentUser == null)
            {
                return null;
            }
            var fullUser = await _context.Users.Where(x => ! 
                                    x.IsDeleted && x.Id == currentUser.Id
            ).Include(u => u.CartItems).ThenInclude(x => x.Product).Include(x => x.Favorites).ThenInclude(x => x.Product).FirstOrDefaultAsync();
            return fullUser;
        }
        public async Task<List<User>> GetUserList()
        {
            return _context.Users.ToList();
        }
        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<bool> IsAdmin(string id)
        {
            var user = await GetUserById(id);
            if (user == null)
            {
                return false;
            }
            return await _userManager.IsInRoleAsync(user, "Administrator");
        }

        public async Task<bool> EditUser(User user)
        {
            var currentUser = await GetUserById(user.Id);

            if(user.ImagePath != null)
            {
                currentUser.ImagePath = user.ImagePath;
                await _context.SaveChangesAsync();
            }

            var setPhoneResult = await _userManager.SetPhoneNumberAsync(currentUser, user.PhoneNumber);
            if (!setPhoneResult.Succeeded)
            {
                return false;
            }

            var setUsernameResult = await _userManager.SetUserNameAsync(currentUser, user.UserName);
            if (!setUsernameResult.Succeeded)
            {
                return false;
            }

            var setEmailResult = await _userManager.SetEmailAsync(currentUser, user.Email);
            if (!setEmailResult.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> EditUserRole(User user, int role)
        {
            var currentUser = await GetUserById(user.Id);
            if (role == 1)
            {
                var removeRoleResult = await _userManager.RemoveFromRoleAsync(currentUser, "Administrator");
                if (!removeRoleResult.Succeeded)
                {
                    return false;
                }
            }
            else if (role == 0)
            {
                var addRoleResult = await _userManager.AddToRoleAsync(currentUser, "Administrator");
                if (!addRoleResult.Succeeded)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> RevertState(string id)
        {
            var user = await GetUserById(id);
            if (user == null)
            {
                return false;
            }
            user.IsDeleted = !user.IsDeleted;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var user = await GetCurrentUser();
            return user.CartItems.ToList();
        }

        public async Task<List<Favorite>> GetFavorites()
        {
            var user = await GetCurrentUser();
            return user.Favorites.ToList();
        }

        public async Task<List<Order>> GetOrderItems()
        {
            var user = await GetCurrentUser();
            return user.Orders.ToList();
        }
    }
}
