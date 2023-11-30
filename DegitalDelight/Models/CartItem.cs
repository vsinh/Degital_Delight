namespace DegitalDelight.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public bool IsDeleted { get; set; } = false;
        public User? User { get; set; }
        public Product? Product { get; set; }

    }
}
