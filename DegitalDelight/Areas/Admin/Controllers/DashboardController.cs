﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DegitalDelight.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        [Area("Admin")]
		[Authorize(Roles = "Administrator")]
		public IActionResult Dashboard()
        {
            return View();
        }
    }
}
