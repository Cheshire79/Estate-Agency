using EstateAgency.DAL.EF;
using EstateAgency.DAL.Entities;
using EstateAgency.DAL.Interface.Identity;
using EstateAgency.DAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Threading.Tasks;

namespace EstateAgency.DAL.Identity.Repository
{
	public class RoleRepository : IRoleRepository
	{
		private ApplicationRoleManager _roleManager;
		public RoleRepository(IdentityDbContext<ApplicationUser> db)
		{
			_roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db)); ;
		}

		public async Task<ApplicationRole> FindByNameAsync(string roleName)
		{
			return await _roleManager.FindByNameAsync(roleName);
		}

		public async Task<ApplicationRole> FindByIdAsync(string roleId)
		{
			return await _roleManager.FindByIdAsync(roleId);
		}

		public async Task<IdentityResult> CreateAsync(ApplicationRole role)
		{
			return await _roleManager.CreateAsync(role);
		}

		public IQueryable<ApplicationRole> Roles()
		{
			return _roleManager.Roles;
		}
	}
}
