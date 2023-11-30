namespace DegitalDelight.Models
{
    public class ReportInventory
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Stock { get; set; }
        public bool IsDeleted { get; set; }
        public Product? Product { get; set; }
    }
}
