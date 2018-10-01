using EstateAgency.DAL.Entities;
using EstateAgency.DAL.Interface.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Ninject.Parameters;

namespace EstateAgency.DAL.Identity.Infrastructure
{
	public class IdentityFactoryRepository : IIdentityFactoryRepository
	{
		private readonly IKernel _kernel;

		public IdentityFactoryRepository(IKernel kernel)
		{
			_kernel = kernel;
		}
		
		public IRoleRepository CreateRoleRepository(IdentityDbContext<ApplicationUser> dataContext)
		{
			return _kernel.Get<IRoleRepository>(new IParameter[] { new ConstructorArgument("context", dataContext) });
		}

		public IUserRepository CreateUserRepository(IdentityDbContext<ApplicationUser> dataContext)
		{
			return _kernel.Get<IUserRepository>(new IParameter[] { new ConstructorArgument("context", dataContext) });

		}
	}
}
