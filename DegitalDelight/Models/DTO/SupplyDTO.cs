namespace DegitalDelight.Models.DTO
{
	public class SupplyDTO
	{
		public int Id { get; set; }
		public int Amount { get; set; }
		public DateTime Date { get; set; }
		public int Price { get; set; }
		public bool IsDeleted { get; set; } = false;
		public int ProductId { get; set; }
	}
}
