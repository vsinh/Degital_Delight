using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Data;
using DegitalDelight.Models;
using Microsoft.EntityFrameworkCore;

namespace DegitalDelight.Areas.Admin.Services
{
	public class CommentService : IComment
	{
		private readonly ApplicationDbContext _context;
		public CommentService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task DeleteComment(int id)
		{
			var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
			if (comment != null)
			{
				comment.IsDeleted = true;
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<Comment>> GetAllComments()
		{
			return await _context.Comments.Include(x => x.User).Include(x => x.Product).ToListAsync();
		}

		public async Task<Comment> GetComments(int id)
		{
			return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<Comment>> SearchComments(string input)
		{
			if (string.IsNullOrEmpty(input))
				return await _context.Comments.Include(x => x.User).Include(x => x.Product).ToListAsync();
			var comments = await _context.Comments.Include(x => x.User).Include(x => x.Product).Where(x =>
				(x.User.UserName).Contains(input)
				|| (x.Product.Name).Contains(input)
				|| (x.Content).Contains(input)
			).ToListAsync();
			return comments;
		}
	}
}
