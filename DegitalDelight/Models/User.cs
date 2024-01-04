using Microsoft.AspNetCore.Identity;

namespace DegitalDelight.Models
{
    public class User : IdentityUser
    {
        public string? ImagePath { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
