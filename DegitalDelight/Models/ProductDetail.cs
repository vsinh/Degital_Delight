namespace DegitalDelight.Models
{
	public class ProductDetail
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string? Property { get; set; }
		public string? Value { get; set; }
		public Product? Product { get; set; }
	}
}
