using AutoMapper;
using EstateAgency.BLL.Interface;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.DAL.Interface.Date;

namespace EstateAgency.BLL.Mapper
{

    public class MapperFactory : IMapperFactory
    {
        private IMapper _mapper { get; set; }
        public MapperFactory()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<City, CityDTO>();
                cfg.CreateMap<CityDTO, City>();
                cfg.CreateMap<CityDistrict, CityDistrictDTO>();
                cfg.CreateMap<CityDistrictDTO, CityDistrict>();
                cfg.CreateMap<Street, StreetDTO>();
                cfg.CreateMap<StreetDTO, Street>();
                cfg.CreateMap<RealEstate, RealEstateDTO>();
                cfg.CreateMap<RealEstateDTO, RealEstate>();
            });
            _mapper = config.CreateMapper();
        }

        public IMapper CreateMapper()
        {
            return _mapper;
        }
    }
}



