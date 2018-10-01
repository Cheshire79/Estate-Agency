using EstateAgency.DAL.Entities;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace EstateAgency.DAL.Interface.Identity
{
	public interface IRoleRepository
	{
		
		Task<ApplicationRole> FindByNameAsync(string roleName);
		Task<ApplicationRole> FindByIdAsync(string roleId);
		Task<IdentityResult> CreateAsync(ApplicationRole role);
		IQueryable<ApplicationRole> Roles();

	}
}
