namespace DegitalDelight.Models
{
    public class ReportDetail
    {
        public int Id { get; set; }
        public int Sales { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ProductType? ProductType { get; set; }
        public Report? Report { get; set; }
    }
}
