using DegitalDelight.Models.DTO;
using DegitalDelight.Models;

namespace DegitalDelight.Areas.Admin.Services.Interfaces
{
	public interface IWarranty
	{
		Task<List<Warranty>> GetAllWarranties();
		Task<Warranty> GetWarranty(int id);
		Task<List<Warranty>> SearchWaranties(string input);
		Task CreateWarranty(Warranty warranty);
		Task EditWarranty(Warranty warranty);
	}
}
