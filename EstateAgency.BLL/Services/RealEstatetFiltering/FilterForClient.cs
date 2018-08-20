using System.Linq;
using EstateAgency.BLL.Interface;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.Client;

namespace EstateAgency.BLL.Services.RealEstateForClientFiltering
{
   public class FilterForClient : IFilterForClient
    {

        public IQueryable<CityDistrictDTO> FilteredDistricts(IQueryable<CityDistrictDTO> districts, ChoosenSearchParametersForClientDTO parameters)
        {
            var result = districts;
            if (parameters.DistrictId.HasValue)
                result = result.Where(x => x.Id == parameters.DistrictId);
            return result;
        }

        public IQueryable<RealEstateDTO> FilteredRealEstates(IQueryable<RealEstateDTO> realEstates, ChoosenSearchParametersForClientDTO parameters)
        {
            var result = realEstates;
            if (parameters.RoomNumber.HasValue)
                result = result.Where(x => x.RoomNumber == parameters.RoomNumber);

            if (parameters.AreaFrom.HasValue)
                result = result.Where(x => x.Area >= parameters.AreaFrom);
            if (parameters.AreaTo.HasValue)
                result = result.Where(x => x.Area <= parameters.AreaTo);

            if (parameters.PriceFrom.HasValue)
                result = result.Where(x => x.Price >= parameters.PriceFrom);
            if (parameters.PriceTo.HasValue)
                result = result.Where(x => x.Price <= parameters.PriceTo);

            if (parameters.HeightFrom.HasValue)
                result = result.Where(x => x.Height >= parameters.HeightFrom);
            if (parameters.HeightTo.HasValue)
                result = result.Where(x => x.Height <= parameters.HeightTo);

            if (parameters.FloorFrom.HasValue)
                result = result.Where(x => x.Floor >= parameters.FloorFrom);
            if (parameters.FloorTo.HasValue)
                result = result.Where(x => x.Floor <= parameters.FloorTo);

            result = result.Where(x => !x.IsSold);
            return result;
        }
    }
}
