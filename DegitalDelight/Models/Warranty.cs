namespace DegitalDelight.Models
{
    public class Warranty
    {
        public int Id { get; set; }
        public string? Name {  get; set; }
        public int Duration { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
		public virtual ICollection<Product> Products { get; set; } = new List<Product>();
	}
}
