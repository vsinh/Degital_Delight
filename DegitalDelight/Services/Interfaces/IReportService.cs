namespace DegitalDelight.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<Tuple<string, int>>> GetInventoryReport();
    }
}
