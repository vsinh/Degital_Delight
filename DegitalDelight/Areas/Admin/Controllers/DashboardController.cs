using DegitalDelight.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DegitalDelight.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class DashboardController : Controller
    {
        private readonly IReportService _reportService;
        public DashboardController(IReportService reportService)
        {
            _reportService = reportService;
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public async Task<IActionResult> GetInventoryReport()
        {
            var result = await _reportService.GetInventoryReport();
            return Json(result);
        }
    }
}
