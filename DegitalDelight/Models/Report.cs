namespace DegitalDelight.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Sales { get; set; }
        public int Revenue { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<ReportDetail> ReportDetails { get; set; } = new List<ReportDetail>();
    }
}
