using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using EstateAgency.DAL.Interface;
using EstateAgency.DAL.Interface.Date;

namespace EstateAgency.DAL.Repository
{
    public class RealEstateRepository : IRepository<RealEstate>
    {
        private IDataContext _db;

        public RealEstateRepository(IDataContext context)
        {
            _db = context;
        }

        public IQueryable<RealEstate> GetAll()
        {
            return _db.RealEstates;
        }

        public async Task<RealEstate> GetByIdAsync(int id)
        {
            return await _db.RealEstates.FindAsync(id);
        }

        public void Create(RealEstate realEstate)
        {
            _db.RealEstates.Add(realEstate);
        }

        public void Delete(RealEstate realEstate)
        {
            _db.RealEstates.Remove(realEstate);
        }

        public void Update(RealEstate realEstate)
        {
            _db.RealEstates.AddOrUpdate(realEstate);
        }
    }
}
