using EstateAgency.DAL.EF;
using EstateAgency.DAL.Entities;
using EstateAgency.DAL.Interface;
using EstateAgency.DAL.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Threading.Tasks;

namespace EstateAgency.DAL.Repository
{
	public class UserReadOnlyRepository : IUserReadOnlyRepository
	{
		private ApplicationUserManager _userManager;

		public UserReadOnlyRepository(DataContext context)
		{
			_userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
		}

		public IQueryable<ApplicationUser> GetAll()
		{
			return _userManager.Users;				
		}

		public async Task<ApplicationUser> GetByIdAsync(string id)
		{
			return await _userManager.FindByIdAsync(id);
		}
	}
}
