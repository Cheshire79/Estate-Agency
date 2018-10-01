using EstateAgency.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace EstateAgency.DAL.Interface
{
	public interface IUserReadOnlyRepository
	{
		IQueryable<ApplicationUser> GetAll();
		Task<ApplicationUser> GetByIdAsync(string id);
	}
}
