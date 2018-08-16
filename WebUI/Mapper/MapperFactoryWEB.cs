
using AutoMapper;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.ForManipulate;
using Identity.BLL.Interface.Data;
using WebUI.Models.Realtor;
using WebUI.Models.Realtor.ForManipulate;
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

                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<Role, RoleViewModel>();

                cfg.CreateMap<UserLoginViewModel, User>().ForMember(x => x.Name,
                    x => x.MapFrom(m => m.UserName)).ForMember(x => x.Password,
                    x => x.MapFrom(m => m.Password));

                cfg.CreateMap<UserRegisterViewModel, User>().ForMember(x => x.Name,
                    x => x.MapFrom(m => m.UserName)).ForMember(x => x.Password,
                    x => x.MapFrom(m => m.Password));

                cfg.CreateMap<CityDistrictDropItemView, CityDistrictDTO>();
                cfg.CreateMap<CityDistrictDTO, CityDistrictDropItemView>();

                cfg.CreateMap<ChoosenSearchParametrsForRealtorView, ChoosenSearchParametersForRealtorDTO>();

                cfg.CreateMap<RealEstateToSaveView, RealEstateDTO>()
                .ForMember(dest => dest.CreationDate, options => options.Ignore())
                .ForMember(dest => dest.IsSold, options => options.Ignore())
                .ForMember(dest => dest.RealtorId, options => options.Ignore());

                cfg.CreateMap<RealEstateForRealtorDTO, RealEstateToSaveView>();

                cfg.CreateMap<EditRealEstateDTO, EditRealEstateView>()
                    .ForMember(x => x.RealEstateForRealtor, x => x.MapFrom(m => m.RealEstate)); 

                cfg.CreateMap<DataForSearchParametersRealtorView, DataForSearchParametersDTO>();
                cfg.CreateMap<DataForManipulateRealEstateDTO, DataForManipulateRealEstateView>().ForMember(dest => dest.ReturnUrl, options => options.Ignore());

            });
            _mapper = config.CreateMapper();
        }

        public IMapper CreateMapperWEB()
        {
            return _mapper;
        }
    }
}



