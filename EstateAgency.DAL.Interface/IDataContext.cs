using System;
using System.Data.Entity;
using System.Threading.Tasks;
using EstateAgency.DAL.Interface.Date;

namespace EstateAgency.DAL.Interface
{
    public interface IDataContext : IDisposable
    {
        DbSet<City> Cities { get; set; }
        DbSet<CityDistrict> CityDistricts { get; set; }
        DbSet<Street> Streets { get; set; }
        DbSet<RealEstate> RealEstates { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
