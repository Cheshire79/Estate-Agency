
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EstateAgency.BLL.Identity.Interface;
using EstateAgency.BLL.Identity.Interface.Data;
using EstateAgency.BLL.Identity.Interface.Data.Validation;
using EstateAgency.DAL.Entities;
using EstateAgency.DAL.Interface.Identity;

using Microsoft.AspNet.Identity;

namespace EstateAgency.BLL.Identity.Services
{
	public class IdentityService : IIdentityService
	{
		private readonly string adminRoleName = "admin";
		private IIdentityUnitOfWork _unitOfWork { get; set; }
		private IMapper _mapper;

		public IdentityService(IIdentityUnitOfWork unitOfWork, IIdentityBLLMapper mapperFactory)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapperFactory.CreateMapper();
		}

		public async Task<OperationDetails> Create(UserDTO userDto)
		{
			ApplicationUser user = await _unitOfWork.UserRepository.FindByNameAsync(userDto.Name);
			if (user == null)
			{
				user = _mapper.Map<UserDTO, ApplicationUser>(userDto);
				user.Id = new ApplicationUser().Id;
				await _unitOfWork.UserRepository.CreateAsync(user, userDto.Password);
				await _unitOfWork.UserRepository.AddToRoleAsync(user.Id, userDto.RoleByDefault);
				await _unitOfWork.SaveAsync();
				return new OperationDetails(true, "Registration successfully passed", "");
			}
			else
			{
				return new OperationDetails(false, "Name " + userDto.Name + " is already taken", "UserName");
			}
		}

		public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
		{
			ClaimsIdentity claim = null;
			ApplicationUser user = await _unitOfWork.UserRepository.FindAsync(userDto.Name, userDto.Password);
			if (user != null)
				claim = await _unitOfWork.UserRepository.CreateIdentityAsync(user,
					DefaultAuthenticationTypes.ApplicationCookie);
			return claim;
		}

		public IQueryable<UserDTO> GetUsers()
		{
			return _unitOfWork.UserRepository.Users().ProjectTo<UserDTO>(_mapper.ConfigurationProvider);
		}

		public IQueryable<RoleDTO> GetRoles()
		{
			return _unitOfWork.RoleRepository.Roles().ProjectTo<RoleDTO>(_mapper.ConfigurationProvider);
		}

		public async Task<OperationDetails> ChangePassword(string userId, string oldPassword, string newPassword)
		{
			IdentityResult result = await _unitOfWork.UserRepository.ChangePasswordAsync(userId, oldPassword, newPassword);

			if (result.Succeeded)
			{
				return new OperationDetails(true, "Your password has been changed.", "");
			}
			else
			{
				return new OperationDetails(false, "Incorrect password", "");
			}
		}

		public async Task<RoleDTO> FindRoleByIdAsync(string id)
		{
			var role = await _unitOfWork.RoleRepository.FindByIdAsync(id);
			return _mapper.Map<ApplicationRole, RoleDTO>(role);
		}

		public async Task<IQueryable<UserDTO>> GetUsersInRoleAsync(string roleId)
		{

			var role = await _unitOfWork.RoleRepository.FindByIdAsync(roleId);
			//   var role = await _unitOfWork.RoleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
			// how to test this using moq ?
			if (role != null)
			{
				return (from user in _unitOfWork.UserRepository.Users()
						where user.Roles.Any(r => r.RoleId == roleId)
						select user).ProjectTo<UserDTO>(_mapper.ConfigurationProvider);
			}
			//todo handle situation with wrong role id !
			return _unitOfWork.UserRepository.Users().ProjectTo<UserDTO>(_mapper.ConfigurationProvider);

		}

		public async Task<OperationDetails> AddUserToRoleAsync(string userId, string roleId)
		{
			
			var role = await FindRoleByIdAsync(roleId);
			if (role == null)
				throw new ArgumentException("There is no role with id " + roleId);
			// need to do the same with user ?
			var added = await _unitOfWork.UserRepository.AddToRoleAsync(userId, role.Name);
			try
			{
				await _unitOfWork.SaveAsync();
			}
			catch (Exception ex)
			{
				throw new ArgumentException("Cannot add user with id=" + userId + " to role with id=" + roleId);//todo
			}

			if (added.Succeeded)
			{
				return new OperationDetails(true, "user was added", "");
			}
			else
			{
				return new OperationDetails(false, "Cannot add user. Something happens", "");
			}
		}

		public async Task SetInitialData()
		{
			// call here seeding the estate Agency
			var roleList = new List<string> { "client", "admin", "realtor" };
			foreach (string roleName in roleList)
			{
				var role = await _unitOfWork.RoleRepository.FindByNameAsync(roleName);
				if (role == null)
				{
					role = new ApplicationRole { Name = roleName };
					await _unitOfWork.RoleRepository.CreateAsync(role);
				}
			}
			await _unitOfWork.SaveAsync();

			var userDto = new UserDTO() { Name = "Admin", Password = "111111" };

			ApplicationUser user = await _unitOfWork.UserRepository.FindByNameAsync(userDto.Name);
			if (user == null)
			{
				user = _mapper.Map<UserDTO, ApplicationUser>(userDto);
				user.Id = new ApplicationUser().Id;
				user.Email = "test@test.com";
				var result = await _unitOfWork.UserRepository.CreateAsync(user, userDto.Password);

				if (result.Succeeded)
					await _unitOfWork.UserRepository.AddToRoleAsync(user.Id, adminRoleName);
				else
				{

					StringBuilder text = new StringBuilder();
					foreach (var item in result.Errors)
						text.Append(item);
					throw new ArgumentException("Cannot create user with 'admin' role. Got next errors: " + text);
				}
				await _unitOfWork.SaveAsync();
			}


		}
		public async Task<OperationDetails> RemoveUserFromRole(string currentUserId, string userId, string roleId)
		{
			try
			{
				var role = await FindRoleByIdAsync(roleId);
				var user = await _unitOfWork.UserRepository.FindByIdAsync(userId);
				if (currentUserId == userId && role.Name == adminRoleName)
				{
					return new OperationDetails(true, "cannot remove youself from admin role", "");
				}
				var added = await _unitOfWork.UserRepository.RemoveFromRoleAsync(userId, role.Name);
				await _unitOfWork.SaveAsync();
				if (added.Succeeded)
				{
					return new OperationDetails(true, "User " + user.UserName + " was removed", "");
				}
				return new OperationDetails(false, "Cannot remove user. Something happens", "");
			}
			catch
			{
				return new OperationDetails(false, "Cannot remove user. Something happens", "");
				//  todo message
			}
		}

		public async Task<OperationDetails> DeleteUser(string currentUserId, string userId)
		{
			try
			{
				if (currentUserId == userId)
				{
					return new OperationDetails(true, "Cannot delete youself", "");
				}
				var deletedProduct =
					await _unitOfWork.UserRepository.DeleteAsync(await _unitOfWork.UserRepository.FindByIdAsync(userId));
				if (deletedProduct.Succeeded)
				{
					return new OperationDetails(true, "user was deleted", "");
				}
				return new OperationDetails(false, "Cannot delete user. Something happens", "");
			}
			catch
			{
				return new OperationDetails(false, "Cannot delete user. Something happens", "");
			}
		}

		public void Dispose()
		{
			_unitOfWork.Dispose();
		}
	}
}
