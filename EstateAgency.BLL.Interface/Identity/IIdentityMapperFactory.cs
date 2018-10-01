using AutoMapper;

namespace EstateAgency.BLL.Identity.Interface
{
    public interface IIdentityMapperFactory
    {
        IMapper CreateMapper();
    }
}
