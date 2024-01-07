using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Services.Interfaces;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace DegitalDelight.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public FavoriteService(ApplicationDbContext applicationDbContext,
            IEmailService emailService,
            IUserService userService)
        {
            _context = applicationDbContext;
            _emailService = emailService;
            _userService = userService;
        }
        public async Task<bool> AddItemToFavorite(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                var favoriteItem = new Favorite();
                favoriteItem.Product = product;

                var currentUser = await _userService.GetCurrentUser();
                favoriteItem.User = currentUser;
                await _context.AddAsync(favoriteItem);
                await _context.SaveChangesAsync();
              

                return true;
            }
            return false;
        }
        public async Task<List<Favorite>> GetFavoriteProducts(string userId)
        {
            var currentUser = await _userService.GetCurrentUser();
            if (currentUser != null)
            {
                var favoriteProducts = await _context.Favorites.Include(x => x.UserId)
                    .Where(f => f.UserId == currentUser.Id)
                    .Include(f => f.Product)
                    .ToListAsync();

                return favoriteProducts;
            }

            return null;
        }
        public async Task<bool> RemoveItemFromFavorite(int productId, string userId)
        {
            var currentUser = await _userService.GetCurrentUser();

            var favoriteItem = await _context.Favorites
                .Where(f => f.ProductId == productId && f.UserId == currentUser.Id)
                .FirstOrDefaultAsync();

            if (favoriteItem != null)
            {
                _context.Favorites.Remove(favoriteItem);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}
