using EstateAgency.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EstateAgency.DAL.Interface.Identity
{

	public interface IIdentityFactoryRepository
	{
		IUserRepository CreateUserRepository(IdentityDbContext<ApplicationUser> applicationContext);
		IRoleRepository CreateRoleRepository(IdentityDbContext<ApplicationUser> applicationContext);
	}
}
