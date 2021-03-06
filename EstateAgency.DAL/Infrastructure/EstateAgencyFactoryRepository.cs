using EstateAgency.DAL.Interface;
using EstateAgency.DAL.Interface.Date;
using Ninject;
using Ninject.Parameters;

namespace EstateAgency.DAL.Infrastructure
{
	public class EstateAgencyFactoryRepositor : IEstateAgencyFactoryRepository
	{
		private readonly IKernel _kernel;

		public EstateAgencyFactoryRepositor(IKernel kernel)
		{
			_kernel = kernel;
		}

		public IRepository<RealEstate> CreateRealEstateRepository(IDataContext dataContext)
		{
			return _kernel.Get<IRepository<RealEstate>>(new IParameter[] { new ConstructorArgument("context", dataContext) });
		}

		public IReadOnlyRepository<City> CreateCityRepository(IDataContext dataContext)
		{
			return _kernel.Get<IReadOnlyRepository<City>>(new IParameter[] { new ConstructorArgument("context", dataContext) });
		}

		public IReadOnlyRepository<CityDistrict> CreateCityDistrictRepository(IDataContext dataContext)
		{
			return _kernel.Get<IReadOnlyRepository<CityDistrict>>(new IParameter[] { new ConstructorArgument("context", dataContext) });
		}

		public IReadOnlyRepository<Street> CreateStreetRepository(IDataContext dataContext)
		{
			return _kernel.Get<IReadOnlyRepository<Street>>(new IParameter[] { new ConstructorArgument("context", dataContext) });
		}

		public IUserReadOnlyRepository CreateUserRepository(IDataContext dataContext)
		{
			return _kernel.Get<IUserReadOnlyRepository>(new IParameter[] { new ConstructorArgument("context", dataContext) });
		}

	}
}