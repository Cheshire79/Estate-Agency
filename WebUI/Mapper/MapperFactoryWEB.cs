
using AutoMapper;
using EstateAgency.BLL.Identity.Interface.Data;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.ForManipulate;
using EstateAgency.BLL.Interface.Date.Realtor;
using WebUI.Models.EstateAgency.ForManipulate;
using WebUI.Models.EstateAgency.Realtor;
using WebUI.Models.Realtor;
using WebUI.Models.UsersAndRoles;

namespace WebUI.Mapper
{
    public class MapperFactoryWEB : IMapperFactoryWEB
    {
        private IMapper _mapper { get; set; }
        public MapperFactoryWEB()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // source , destination

                cfg.CreateMap<UserDTO, UserViewModel>();
                cfg.CreateMap<RoleDTO, RoleViewModel>();

                cfg.CreateMap<UserLoginViewModel, UserDTO>().ForMember(x => x.Name,
                    x => x.MapFrom(m => m.UserName)).ForMember(x => x.Password,
                    x => x.MapFrom(m => m.Password));

                cfg.CreateMap<UserRegisterViewModel, UserDTO>().ForMember(x => x.Name,
                    x => x.MapFrom(m => m.UserName)).ForMember(x => x.Password,
                    x => x.MapFrom(m => m.Password));

                cfg.CreateMap<CityDistrictDropItemView, CityDistrictDTO>();
                cfg.CreateMap<CityDistrictDTO, CityDistrictDropItemView>();

                cfg.CreateMap<ChoosenSearchParametersForRealtorView, ChoosenSearchParametersForRealtorDTO>();

                cfg.CreateMap<RealEstateToSaveView, RealEstateDTO>()
                .ForMember(dest => dest.CreationDate, options => options.Ignore())
                .ForMember(dest => dest.IsSold, options => options.Ignore())
                .ForMember(dest => dest.RealtorId, options => options.Ignore());

                cfg.CreateMap<RealEstateForRealtorDTO, RealEstateToSaveView>();

                cfg.CreateMap<EditRealEstateDTO, EditRealEstateView>()
                    .ForMember(x => x.RealEstateForRealtor, x => x.MapFrom(m => m.RealEstate)); 

                cfg.CreateMap<DataForSearchParametersRealtorView, DataForSearchParametersRealtorDTO>();
                cfg.CreateMap<DataForManipulateRealEstateDTO, DataForManipulateRealEstateView>().ForMember(dest => dest.ReturnUrl, options => options.Ignore());

				cfg.CreateMap<DataAboutRealEstatesForRealtorDTO, DataAboutRealEstatesForRealtorView>();
			});
            _mapper = config.CreateMapper();
        }

        public IMapper CreateMapperWEB()
        {
            return _mapper;
        }
    }
}



