using System;
using System.Threading.Tasks;

namespace EstateAgency.DAL.Interface.Identity
{
	public interface IIdentityUnitOfWork : IDisposable
	{
		IUserRepository UserRepository { get; }
		IRoleRepository RoleRepository { get; }

		Task SaveAsync();
	}
}
