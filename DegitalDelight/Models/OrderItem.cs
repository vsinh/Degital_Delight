namespace DegitalDelight.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string? State { get; set; }
        public int Amount { get; set; }
        public int Cost { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Product? Product { get; set; }
        public Order? Order { get; set; }
        public Discount? DiscountItem { get; set; }
    }
}
