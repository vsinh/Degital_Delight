using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DegitalDelight.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrderService orderService;

        public ReportService(ApplicationDbContext context, IOrderService orderService)
        {
            _context = context;
            this.orderService = orderService;
        }

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
