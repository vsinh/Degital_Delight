using DegitalDelight.Models;
using DegitalDelight.Models.DTO;

namespace DegitalDelight.Areas.Admin.Services.Interfaces
{
	public interface ISupply
	{
		Task<List<Supply>> GetAllSupplies();
		Task<Supply> GetSupplies(int id);
		Task<List<Supply>> SearchSupplies(string input);
		Task CreateSupplies(Supply supply);
		Task EditSupply(SupplyDTO supply);
		Task DeleteSupply(int id);
	}
}
