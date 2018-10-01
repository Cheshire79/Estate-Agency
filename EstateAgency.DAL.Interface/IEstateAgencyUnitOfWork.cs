using System;
using System.Threading.Tasks;
using EstateAgency.DAL.Interface.Date;

namespace EstateAgency.DAL.Interface
{
    public interface IEstateAgencyUnitOfWork : IDisposable
    {
        IRepository<RealEstate> RealEstates { get; }
        IReadOnlyRepository<City> Cities { get; }
        IReadOnlyRepository<CityDistrict> CityDistricts { get; }
        IReadOnlyRepository<Street> Streets { get; }
		IUserReadOnlyRepository Users	{	get;	}

			Task SaveAsync();
    }
}
