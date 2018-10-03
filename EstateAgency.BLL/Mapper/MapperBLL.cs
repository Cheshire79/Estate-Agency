using AutoMapper;
using EstateAgency.BLL.Identity.Interface.Data;
using EstateAgency.BLL.Interface;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.DAL.Entities;
using EstateAgency.DAL.Interface.Date;

namespace EstateAgency.BLL.Mapper
{

    public class BLLMapper : IBLLMapper
    {
        private IMapper _mapper { get; set; }
        public BLLMapper()
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

				cfg.CreateMap<ApplicationUser, UserDTO>().ForMember(x => x.Name,
				  x => x.MapFrom(m => m.UserName)).ForMember(x => x.Id,
				  x => x.MapFrom(m => m.Id)).ForMember(x => x.Email,
				  x => x.MapFrom(m => m.Email)); ;
			});
            _mapper = config.CreateMapper();
        }

        public IMapper CreateMapper()
        {
            return _mapper;
        }
    }
}



