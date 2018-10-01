using EstateAgency.DAL.EF;
using EstateAgency.DAL.Interface.Identity;
using System.Threading.Tasks;

namespace EstateAgency.DAL.Identity.Repository
{
	public class IdentityUnitOfWork : IIdentityUnitOfWork
	{
		private readonly DataContext _db;
		private readonly IIdentityFactoryRepository _factoryRepository;
		private IUserRepository _userRepository;
		private IRoleRepository _roleRepository;

		public IdentityUnitOfWork(DataContext applicationContext, IIdentityFactoryRepository factoryRepository)
		{
			_db = applicationContext;
			_factoryRepository = factoryRepository;
		}

		public IUserRepository UserRepository
		{
			get { return _userRepository ?? (_userRepository = _factoryRepository.CreateUserRepository(_db)); }
		}

		public IRoleRepository RoleRepository
		{
			get { return _roleRepository ?? (_roleRepository = _factoryRepository.CreateRoleRepository(_db)); }
		}
		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}


		public void Dispose()
		{
			_db?.Dispose();
		}
	}
}
