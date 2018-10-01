using System.Threading.Tasks;
using EstateAgency.DAL.EF;
using EstateAgency.DAL.Interface;
using EstateAgency.DAL.Interface.Date;

namespace EstateAgency.DAL.Repository
{
    public class EstateAgencyUnitOfWork : IEstateAgencyUnitOfWork
    {
        private IDataContext _db;
        private IEstateAgencyFactoryRepository _factoryRepository;

        public EstateAgencyUnitOfWork(IEstateAgencyFactoryRepository factoryRepository, DataContext db)
        {
            _factoryRepository = factoryRepository;
            _db = db;
        }

        private IRepository<RealEstate> _realEstateRepository;
        private IReadOnlyRepository<City> _cityRepository;
        private IReadOnlyRepository<CityDistrict> _cityDistrictRepository;
        private IReadOnlyRepository<Street> _streetRepository;
		public IUserReadOnlyRepository _usersRepository;


		public IUserReadOnlyRepository Users
		{
			get { return _usersRepository ?? (_usersRepository = _factoryRepository.CreateUserRepository(_db)); }
		}


		public IRepository<RealEstate> RealEstates
        {
            get { return _realEstateRepository ?? (_realEstateRepository = _factoryRepository.CreateRealEstateRepository(_db)); }
        }

        public IReadOnlyRepository<City> Cities
        {
            get { return _cityRepository ?? (_cityRepository = _factoryRepository.CreateCityRepository(_db)); }
        }
        public IReadOnlyRepository<CityDistrict> CityDistricts
        {
            get { return _cityDistrictRepository ?? (_cityDistrictRepository = _factoryRepository.CreateCityDistrictRepository(_db)); }
        }
        public IReadOnlyRepository<Street> Streets
        {
            get { return _streetRepository ?? (_streetRepository = _factoryRepository.CreateStreetRepository(_db)); }
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
