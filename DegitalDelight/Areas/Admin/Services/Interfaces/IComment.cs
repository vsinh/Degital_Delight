using DegitalDelight.Models;
using DegitalDelight.Models.DTO;

namespace DegitalDelight.Areas.Admin.Services.Interfaces
{
	public interface IComment
	{
		Task<List<Comment>> GetAllComments();
		Task<Comment> GetComments(int id);
		Task<List<Comment>> SearchComments(string input);
		Task DeleteComment(int id);
	}
}
