using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

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

            return currentUser;
        }
    }
}
