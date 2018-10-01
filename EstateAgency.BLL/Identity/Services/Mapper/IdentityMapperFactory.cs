using AutoMapper;
using EstateAgency.BLL.Identity.Interface;
using EstateAgency.BLL.Identity.Interface.Data;
using EstateAgency.DAL.Entities;


namespace Identity.BLL.Mapper
{
    public class IdentityMapperFactory : IIdentityMapperFactory
	{
        private IMapper _mapper { get; set; }
        public IdentityMapperFactory()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, User>().ForMember(x => x.Name,
                    x => x.MapFrom(m => m.UserName)).ForMember(x => x.Id,
                    x => x.MapFrom(m => m.Id)).ForMember(x => x.Email,
                    x => x.MapFrom(m => m.Email)); ;

                cfg.CreateMap<ApplicationRole, Role>().ForMember(x => x.Name,
                    x => x.MapFrom(m => m.Name)).ForMember(x => x.Id,
                    x => x.MapFrom(m => m.Id));

                cfg.CreateMap<User, ApplicationUser>().ForMember(x => x.UserName,
                    x => x.MapFrom(m => m.Name)).ForMember(x => x.Id,
                    x => x.MapFrom(m => m.Id)).ForMember(x => x.Email,
                    x => x.MapFrom(m => m.Email));

            });
            _mapper = config.CreateMapper();
        }

        public IMapper CreateMapper()
        {
            return _mapper;
        }
    }
}



