using EstateAgency.DAL.EF;
using EstateAgency.DAL.Entities;
using EstateAgency.DAL.Interface.Identity;
using EstateAgency.DAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EstateAgency.DAL.Identity.Repository
{
	public class UserRepository : IUserRepository
	{
		private ApplicationUserManager _userManager;

		public UserRepository(IdentityDbContext<ApplicationUser> db)
		{
			_userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));

			//todo
			// Config user login validator
			_userManager.UserValidator = new UserValidator<ApplicationUser>(_userManager)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};

			// Config user password validator
			_userManager.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 4,
				RequireNonLetterOrDigit = false,
				RequireDigit = true,
				RequireLowercase = false,
				RequireUppercase = false,
			};

			// Config user Lockout
			_userManager.UserLockoutEnabledByDefault = true;
			_userManager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
			_userManager.MaxFailedAccessAttemptsBeforeLockout = 5;
		}

		#region IUserRepository Members
		public async Task<ApplicationUser> FindAsync(string userName, string password)
		{
			return await _userManager.FindAsync(userName, password);
		}

		public async Task<ApplicationUser> FindByIdAsync(string userId)
		{
			return await _userManager.FindByIdAsync(userId);
		}

		public async Task<ApplicationUser> FindByNameAsync(string userName)
		{
			return await _userManager.FindByNameAsync(userName);
		}

		public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
		{
			return await _userManager.CreateAsync(user, password);
		}

		public async Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType)
		{
			return await _userManager.CreateIdentityAsync(user, authenticationType);
		}

		//public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
		//{
		//	return await _userManager.UpdateAsync(user);
		//}

		public async Task<IdentityResult> DeleteAsync(ApplicationUser user)
		{
			return await _userManager.DeleteAsync(user);
		}

		public async Task<IdentityResult> AddToRoleAsync(string userId, string role)
		{
			return await _userManager.AddToRoleAsync(userId, role);
		}

		public async Task<IdentityResult> RemoveFromRoleAsync(string userId, string role)
		{
			return await _userManager.RemoveFromRoleAsync(userId, role);
		}
		#endregion
		//public IQueryable<City> GetAll()
		public IQueryable<ApplicationUser> Users()
		{
			return _userManager.Users;
		}

		public async Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
		{
			return await _userManager.ChangePasswordAsync(userId, oldPassword, newPassword);
		}
	}
}
