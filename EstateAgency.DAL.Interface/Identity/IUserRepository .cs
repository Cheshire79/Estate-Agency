using EstateAgency.DAL.Entities;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EstateAgency.DAL.Interface.Identity
{
	public interface IUserRepository
	{
		Task<ApplicationUser> FindAsync(string userName, string password);
		Task<ApplicationUser> FindByIdAsync(string userId);
		Task<ApplicationUser> FindByNameAsync(string userName);
		Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
		Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType);
		//Task<IdentityResult> UpdateAsync(ApplicationUser user);
		Task<IdentityResult> DeleteAsync(ApplicationUser user);
		Task<IdentityResult> AddToRoleAsync(string userId, string role);
		Task<IdentityResult> RemoveFromRoleAsync(string userId, string role);

		IQueryable<ApplicationUser> Users();
		Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword);

	}
}
