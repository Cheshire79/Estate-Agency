using System.Data.Entity;
using System.Linq;
using EstateAgency.DAL.Interface;
using EstateAgency.DAL.Interface.Date;

namespace EstateAgency.DAL.EF
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(string connection)
            : base(connection)
        {
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<CityDistrict> CityDistricts { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }


        static DataContext()
        {
            Database.SetInitializer<DataContext>(new StoreDbInitializer());
        }
    }

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext db)
        {

            db.Cities.Add(new City() { Name = "Киев" });
            db.Cities.Add(new City() { Name = "Черкассы" });
            db.SaveChanges();
            var city1 = db.Cities.FirstOrDefault(x => x.Name == "Киев");
            if (city1 != null)
            {
                db.CityDistricts.Add(new CityDistrict()
                {
                    CityId = city1.Id,
                    Name = "Троещина"
                });
                db.CityDistricts.Add(new CityDistrict()
                {
                    CityId = city1.Id,
                    Name = "Шевченковский р-н"
                });
                db.CityDistricts.Add(new CityDistrict()
                {
                    CityId = city1.Id,
                    Name = "Голосеевский р-н"
                });
                db.CityDistricts.Add(new CityDistrict()
                {
                    CityId = city1.Id,
                    Name = "Днепровский р-н"
                });
            }

            db.SaveChanges();
            var сityDistrict1 = db.CityDistricts.FirstOrDefault(x => x.Name == "Шевченковский р-н");
            if (сityDistrict1 != null)
            {
                db.Streets.Add(new Street()
                {
                    CityDistrictId = сityDistrict1.Id,
                    Name = "Чорновола ул."
                });
                db.Streets.Add(new Street()
                {
                    CityDistrictId = сityDistrict1.Id,
                    Name = "Саксаганского ул."
                });
                db.Streets.Add(new Street()
                {
                    CityDistrictId = сityDistrict1.Id,
                    Name = "Ружинская ул."
                });
                db.Streets.Add(new Street()
                {
                    CityDistrictId = сityDistrict1.Id,
                    Name = "Стеценко ул."
                });
                db.Streets.Add(new Street()
                {
                    CityDistrictId = сityDistrict1.Id,
                    Name = "Победы просп."
                });
            }

            сityDistrict1 = db.CityDistricts.FirstOrDefault(x => x.Name == "Днепровский р-н");
            if (сityDistrict1 != null)
            {
                db.Streets.Add(new Street()
                {
                    CityDistrictId = сityDistrict1.Id,
                    Name = "Жмаченко генерала ул."
                });
                db.Streets.Add(new Street()
                {
                    CityDistrictId = сityDistrict1.Id,
                    Name = "Строителей ул."
                });
                db.Streets.Add(new Street()
                {
                    CityDistrictId = сityDistrict1.Id,
                    Name = "Тампере ул."
                });
            }
            var city2 = db.Cities.FirstOrDefault(x => x.Name == "Черкассы");
            if (city2 != null)
            {
                db.CityDistricts.Add(new CityDistrict()
                {
                    CityId = city2.Id,
                    Name = "Приднепровский"
                });
                db.CityDistricts.Add(new CityDistrict()
                {
                    CityId = city2.Id,
                    Name = "Сосновский"
                });
            }
            db.SaveChanges();
        }
    }


}