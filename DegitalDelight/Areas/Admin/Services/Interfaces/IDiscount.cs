using DegitalDelight.Models;
using DegitalDelight.Models.DTO;

namespace DegitalDelight.Areas.Admin.Services.Interfaces
{
	public interface IDiscount
	{
		Task<List<Discount>> GetAllDiscounts();
		Task<Discount> GetDiscountById(int id);
		Task<List<Discount>> SearchDiscounts(string input);
		Task CreateDisount(Discount supply);
		Task EditDiscount(Discount supply);
		Task DeleteDiscount(int id);
	}
}
