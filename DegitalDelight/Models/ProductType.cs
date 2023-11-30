namespace DegitalDelight.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<ReportDetail> ReportDetails { get; set; } = new List<ReportDetail>();
    }
}
