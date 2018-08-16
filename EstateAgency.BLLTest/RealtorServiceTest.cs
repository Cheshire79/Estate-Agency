using System;
using System.Collections.Generic;
using System.Linq;
using EstateAgency.BLL.Interface;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Mapper;
using EstateAgency.BLL.Services;
using EstateAgency.BLL.Services.RealeEstateOrdering;
using EstateAgency.DAL.Interface;
using EstateAgency.DAL.Interface.Date;
using Moq;
using NUnit.Framework;

namespace EstateAgency.BLLTest
{
    [TestFixture]
    public class RealtorServiceTest
    {
        private IRealtorService _realtorService;
        [SetUp]
        public void SetUp()
        {

            var listCitis = new City[]
            {
                new City() {Id = 1, Name = "Киев"},
                new City() {Id = 2, Name = "Черкассы"},
            };

            Mock<IReadOnlyRepository<City>> cityReadOnlyRepository = new Mock<IReadOnlyRepository<City>>();
            cityReadOnlyRepository.Setup(m => m.GetAll()).Returns(listCitis.AsQueryable());
            cityReadOnlyRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    return cityReadOnlyRepository.Object.GetAll().FirstOrDefault(x => x.Id == id);
                });


            Mock<IReadOnlyRepository<CityDistrict>> cityDistrictReadOnlyRepository = new Mock<IReadOnlyRepository<CityDistrict>>();
            var listCityDistrict = new List<CityDistrict>();
            var city_1 = cityReadOnlyRepository.Object.GetAll().FirstOrDefault(x => x.Name == "Киев");
            int cityDistrictId = 1;
            if (city_1 != null)
            {
                listCityDistrict.Add(new CityDistrict()
                {
                    CityId = city_1.Id,
                    Name = "Троещина",
                    Id = cityDistrictId++
                });

                listCityDistrict.Add(new CityDistrict()
                {
                    CityId = city_1.Id,
                    Name = "Шевченковский р-н",
                    Id = cityDistrictId++
                });

                listCityDistrict.Add(new CityDistrict()
                {
                    CityId = city_1.Id,
                    Name = "Голосеевский р-н",
                    Id = cityDistrictId++
                });

                listCityDistrict.Add(new CityDistrict()
                {
                    CityId = city_1.Id,
                    Name = "Днепровский р-н",
                    Id = cityDistrictId++
                });
            }

            cityDistrictReadOnlyRepository.Setup(m => m.GetAll()).Returns(listCityDistrict.AsQueryable());
            cityDistrictReadOnlyRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    return cityDistrictReadOnlyRepository.Object.GetAll().FirstOrDefault(x => x.Id == id);
                });

            Mock<IReadOnlyRepository<Street>> streetReadOnlyRepository = new Mock<IReadOnlyRepository<Street>>();
            var listStreet = new List<Street>();

            var district_1 = cityDistrictReadOnlyRepository.Object.GetAll().FirstOrDefault(x => x.Name == "Шевченковский р-н");
            int streetId = 1;
            if (district_1 != null)
            {
                listStreet.Add(new Street()
                {
                    CityDistrictId = district_1.Id,
                    Name = "Чорновола ул.",
                    Id = streetId++
                });

                listStreet.Add(new Street()
                {
                    CityDistrictId = district_1.Id,
                    Name = "Саксаганского ул.",
                    Id = streetId++
                });

                listStreet.Add(new Street()
                {
                    CityDistrictId = district_1.Id,
                    Name = "Ружинская ул.",
                    Id = streetId++
                });

                listStreet.Add(new Street()
                {
                    CityDistrictId = district_1.Id,
                    Name = "Стеценко ул.",
                    Id = streetId++
                });

                listStreet.Add(new Street()
                {
                    CityDistrictId = district_1.Id,
                    Name = "Победы просп.",
                    Id = streetId++
                });
            }

            var district_2 = cityDistrictReadOnlyRepository.Object.GetAll().FirstOrDefault(x => x.Name == "Днепровский р-н");

            if (district_2 != null)
            {
                listStreet.Add(new Street()
                {
                    CityDistrictId = district_2.Id,
                    Name = "Жмаченко генерала ул.",
                    Id = streetId++
                });

                listStreet.Add(new Street()
                {
                    CityDistrictId = district_2.Id,
                    Name = "Строителей ул.",
                    Id = streetId++
                });

                listStreet.Add(new Street()
                {
                    CityDistrictId = district_2.Id,
                    Name = "Тампере ул.",
                    Id = streetId++
                });

            }

            streetReadOnlyRepository.Setup(m => m.GetAll()).Returns(listStreet.AsQueryable());
            streetReadOnlyRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    return streetReadOnlyRepository.Object.GetAll().FirstOrDefault(x => x.Id == id);
                });


            Mock<IRepository<RealEstate>> realEstateRepository = new Mock<IRepository<RealEstate>>();
            var listRealEstate = new List<RealEstate>();

            var street_1 = streetReadOnlyRepository.Object.GetAll().FirstOrDefault(x => x.Name == "Чорновола ул.");
            int realEstateId = 1;
            if (street_1 != null)
            {
                listRealEstate.Add(new RealEstate()
                {
                    Building = "33/30",
                    Appartment = 24,
                    Floor = 6,
                    Height = 9,
                    Area = 54,
                    Price = 36.000M,
                    RoomNumber = 2,
                    CreationDate = DateTime.Now,
                    Description = "test",
                    IsSold = false,
                    StreetId = street_1.Id,
                    RealtorId = "test",
                    Id = realEstateId++
                });
                listRealEstate.Add(new RealEstate()
                {
                    Building = "3",
                    Appartment = 4,
                    Floor = 2,
                    Height = 19,
                    Area = 154,
                    Price = 56.000M,
                    RoomNumber = 2,
                    CreationDate = DateTime.Now,
                    Description = "test",
                    IsSold = false,
                    StreetId = street_1.Id,
                    RealtorId = "test",
                    Id = realEstateId++
                });
            }

            var street_2 = streetReadOnlyRepository.Object.GetAll().FirstOrDefault(x => x.Name == "Саксаганского ул.");
            if (street_2 != null)
            {
                listRealEstate.Add(new RealEstate()
                {
                    Building = "33/30",
                    Appartment = 24,
                    Floor = 6,
                    Height = 9,
                    Area = 54,
                    Price = 36.000M,
                    RoomNumber = 2,
                    CreationDate = DateTime.Now,
                    Description = "test",
                    IsSold = false,
                    StreetId = street_2.Id,
                    RealtorId = "test",
                    Id = realEstateId++
                });
                listRealEstate.Add(new RealEstate()
                {
                    Building = "123",
                    Appartment = 94,
                    Floor = 12,
                    Height = 19,
                    Area = 154,
                    Price = 516.000M,
                    RoomNumber = 4,
                    CreationDate = DateTime.Now,
                    Description = "test",
                    IsSold = false,
                    StreetId = street_2.Id,
                    RealtorId = "test",
                    Id = realEstateId++
                });
            }


            realEstateRepository.Setup(m => m.GetAll()).Returns(listRealEstate.AsQueryable());
            realEstateRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    return realEstateRepository.Object.GetAll().FirstOrDefault(x => x.Id == id);
                });

            var unitOfWork = new Mock<IUnitOfWork>();

            unitOfWork.Setup(x => x.Cities).Returns(cityReadOnlyRepository.Object);
            unitOfWork.Setup(x => x.CityDistricts).Returns(cityDistrictReadOnlyRepository.Object);
            unitOfWork.Setup(x => x.Streets).Returns(streetReadOnlyRepository.Object);
            unitOfWork.Setup(x => x.RealEstates).Returns(realEstateRepository.Object);

 
            _realtorService = new RealtorService(unitOfWork.Object, new MapperFactory(), new RealeEstateSort());

        }

        [Test]
        public void GetRealEstateAll()
        {
            int realEstateAmount=4;
            var realEstate = _realtorService.GetRealEstatesForRealtor("", new ChoosenSearchParametersForRealtorDTO())
                .ToList();
            Assert.IsTrue(realEstate.Count == realEstateAmount);
        }

        [Test]
        public void RealEstatePriceFilter()
        {
            int filteredRealEstateAmount = 1;
            var searchParameters = new ChoosenSearchParametersForRealtorDTO() {PriceFrom = 50.000M, PriceTo = 60.000M};
            var realEstate = _realtorService.GetRealEstatesForRealtor("", searchParameters).ToList();
            Assert.IsTrue(realEstate.Count == filteredRealEstateAmount);
        }

        [Test]
        public void RealEstatePriceFilterWithEquelsBorderValue()
        {
            int filteredRealEstateAmount = 3;
            var searchParameters = new ChoosenSearchParametersForRealtorDTO() { PriceFrom = 36.000M, PriceTo = 56.000M };
            var realEstate = _realtorService.GetRealEstatesForRealtor("", searchParameters).ToList();
            Assert.IsTrue(realEstate.Count == filteredRealEstateAmount);
        }
    }
}
