namespace DegitalDelight.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public string? Picture { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ProductType? ProductType { get; set; }
        public Warranty? Warranty { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public virtual ICollection<ReportInventory> ReportInventories { get; set; } = new List<ReportInventory>();
        public virtual ICollection<Supply> Supplies { get; } = new List<Supply>();
    }
}