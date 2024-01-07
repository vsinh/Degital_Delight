using DegitalDelight.Models;
using DegitalDelight.Models.DTO;

namespace DegitalDelight.Areas.Admin.Services.Interfaces
{
	public interface IDiscount
	{
		Task<List<Discount>> GetAllDiscounts();
		Task<Discount> GetDiscountById(int id);
		Task<List<Discount>> SearchProductDiscounts(string input);
		Task<List<Discount>> SearchCodeDiscounts(string input);
		Task CreateDiscount(Discount discount);
		Task CreateProductDiscount(Discount discount);
		Task EditDiscount(Discount discount);
		Task DeleteDiscount(int id);
		Task<bool> CheckExistCode(Discount discount);
	}
}
