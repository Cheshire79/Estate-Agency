using System.Linq;
using EstateAgency.BLL.Interface;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.Realtor;

namespace EstateAgency.BLL.Services.RealEstatetFiltering
{
    public class FilterForRealtor : IFilterForRealtor
    {
        public IQueryable<CityDistrictDTO> FilteredDistricts(IQueryable<CityDistrictDTO> districts, ChoosenSearchParametersForRealtorDTO parameters)
        {
            var result = districts;
            if (parameters.DistrictId.HasValue)
                result = result.Where(x => x.Id == parameters.DistrictId);
            return result;
        }

        public IQueryable<RealEstateDTO> FilteredRealEstates(IQueryable<RealEstateDTO> realEstates, ChoosenSearchParametersForRealtorDTO parameters, string userId)
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

            if (parameters.ShowOnlyMyOwn)
                result = result.Where(x => x.RealtorId == userId);

            return result;
        }

    }
}
