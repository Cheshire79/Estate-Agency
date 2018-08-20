using System.Linq;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.Client;

namespace EstateAgency.BLL.Interface
{
    public interface IFilterForClient
    {
       IQueryable<RealEstateDTO> FilteredRealEstates(IQueryable<RealEstateDTO> realEstates,
            ChoosenSearchParametersForClientDTO parameters);

        IQueryable<CityDistrictDTO> FilteredDistricts(IQueryable<CityDistrictDTO> districts,
            ChoosenSearchParametersForClientDTO parameters);
    }
}
