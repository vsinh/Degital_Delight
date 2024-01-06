namespace DegitalDelight.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Type { get; set; }
        public int Amount { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Product? Product { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
