using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Areas.Admin.Services;
using DegitalDelight.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DegitalDelight.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CommentController : Controller
	{
		private readonly ApplicationDbContext _context;
        private readonly IComment _commentService;

        public CommentController(ApplicationDbContext context, IComment commentService)
		{
			_context = context;
			_commentService = commentService;
		}
		public async Task<IActionResult> Index()
		{
			var comments = await _context.Comments.Include(x => x.User).Include(x => x.Product).ToListAsync();
			return View(comments);
		}
		[HttpGet]
        public async Task<IActionResult> Search(string input)
        {
            var comments = await _commentService.SearchComments(input);
            return Json(comments);
        }
		public async Task<IActionResult> Delete()
		{
			return RedirectToAction("Index");
		}
	}
}
