using System.Linq;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.Realtor;

namespace EstateAgency.BLL.Interface
{
    public interface IFilterForRealtor
    {
        IQueryable<CityDistrictDTO> FilteredDistricts(IQueryable<CityDistrictDTO> districts,
           ChoosenSearchParametersForRealtorDTO parameters);
        IQueryable<RealEstateDTO> FilteredRealEstates(IQueryable<RealEstateDTO> realEstates,
            ChoosenSearchParametersForRealtorDTO parameters, string userId);
    }
}
