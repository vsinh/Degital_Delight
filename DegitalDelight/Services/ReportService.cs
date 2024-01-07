using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DegitalDelight.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task CreateMonthlyReport()
        //{
        //    //var report = new Report();
        //    //report.Month = DateTime.Now.Month;
        //    //report.Year = DateTime.Now.Year;
        //    //foreach (var item in await _context.ProductTypes.Include(x => x.Products).ThenInclude(x => x.Supplies).ToListAsync())
        //    //{
        //    //    var reportDetail = new ReportDetail();
        //    //    reportDetail.Report = report;
        //    //    reportDetail.ProductType = item;
        //    //    int sales = 0;
        //    //    foreach (var product in item.Products)
        //    //    {
        //    //        int productSales = 0;
        //    //        int supplyAmount = 0;
        //    //        foreach (var supply in product.Supplies)
        //    //        {
        //    //            if (DateTime.Now - supply.Date > TimeSpan.FromDays(30))
        //    //            {
        //    //                supplyAmount += supply.Amount;
        //    //            }
        //    //        }
        //    //        productSales = product.Stock - supplyAmount;
        //    //    }
        //    //    reportDetail.Sales = sales;
        //    //}
        //}

        public async Task<List<Tuple<string, int>>> GetInventoryReport()
        {
            List<Tuple<string, int>> mylist = new List<Tuple<string, int>>();
            foreach (var productType in await _context.ProductTypes.Where(x => !x.IsDeleted).Include(x => x.Products).ToListAsync())
            {
                int stock = 0;
                foreach (var product in productType.Products.Where(x => !x.IsDeleted).ToList())
                {
                    stock += product.Stock;
                }
                mylist.Add(new Tuple<string, int>(productType.Name, stock));
            }
            return mylist;
        }
    }
}
