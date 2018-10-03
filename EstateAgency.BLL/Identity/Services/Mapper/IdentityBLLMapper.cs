using AutoMapper;
using EstateAgency.BLL.Identity.Interface;
using EstateAgency.BLL.Identity.Interface.Data;
using EstateAgency.DAL.Entities;


namespace Identity.BLL.Mapper
{
    public class IdentityBLLMapper : IIdentityBLLMapper
	{
        private IMapper _mapper { get; set; }
        public IdentityBLLMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserDTO>().ForMember(x => x.Name,
                    x => x.MapFrom(m => m.UserName)).ForMember(x => x.Id,
                    x => x.MapFrom(m => m.Id)).ForMember(x => x.Email,
                    x => x.MapFrom(m => m.Email)); ;

                cfg.CreateMap<ApplicationRole, RoleDTO>().ForMember(x => x.Name,
                    x => x.MapFrom(m => m.Name)).ForMember(x => x.Id,
                    x => x.MapFrom(m => m.Id));

                cfg.CreateMap<UserDTO, ApplicationUser>().ForMember(x => x.UserName,
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



