namespace DegitalDelight.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? State { get; set; }
        public bool IsDeleted { get; set; } = false;
        public User? User { get; set; }
        public Discount? Discount { get; set; }
    }
}
