using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EstateAgency.BLL.Interface;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.Client;
using EstateAgency.BLL.Interface.Date.ForManipulate;
using EstateAgency.DAL.Interface;

namespace EstateAgency.BLL.Services
{
    public class ClientService : IClientService
    {

        private IRealeEstateSort<RealEstateForClientDTO> _realeEstateSort;
        private IRealEstatesDataMapper _realEstatesData;

        public ClientService( IRealeEstateSort<RealEstateForClientDTO> realeEstateSort,  IRealEstatesDataMapper realEstatesData)
        {
            _realeEstateSort = realeEstateSort;
            _realEstatesData = realEstatesData;
        }

        public IQueryable<RealEstateForClientDTO> FormRealEstates(ChoosenSearchParametersForClientDTO parameters)
        {
            IQueryable<RealEstateForClientDTO> realEstates =
                from realEstate in FilteredRealEstates(_realEstatesData.RealEstates(), parameters)
                 join street in _realEstatesData.Streets() on realEstate.StreetId equals street.Id
                 join district in FilteredDistricts(_realEstatesData.KievDistricts(), parameters) on street.CityDistrictId equals district.Id
                 select new RealEstateForClientDTO
                 {
                     Id = realEstate.Id,
                     Building = realEstate.Building,
                     Floor = realEstate.Floor,
                     Height = realEstate.Height,
                     Area = realEstate.Area,
                     Price = realEstate.Price,
                     RoomNumber = realEstate.RoomNumber,
                     CreationDate = realEstate.CreationDate,
                     Description = realEstate.Description,
                     RealtorId = realEstate.RealtorId,
                     StreetName = street.Name,
                     DistrictName = district.Name,
                     Image = realEstate.Image,
                     DistrictId = district.Id,
                     StreetId = street.Id
                 };
            return _realeEstateSort.Sort(parameters.SortOrder)(realEstates);
        }

        private IQueryable<CityDistrictDTO> FilteredDistricts(IQueryable<CityDistrictDTO> districts, ChoosenSearchParametersForClientDTO parameters)
        {
            var result = districts;
            if (parameters.DistrictId.HasValue)
                result = result.Where(x => x.Id == parameters.DistrictId);
            return result;
        }

        private IQueryable<RealEstateDTO> FilteredRealEstates(IQueryable<RealEstateDTO> realEstates, ChoosenSearchParametersForClientDTO parameters)
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

        public async Task<DataForSearchParametersClientDTO> InitiateSearchParameters()
        {
            var searchParameters = new DataForSearchParametersClientDTO();
            searchParameters.Districts = new List<CityDistrictDropDownItemDTO>() { new CityDistrictDropDownItemDTO() { Id = null, Name = "No matter" } };
            searchParameters.Districts.AddRange(await _realEstatesData.KievDistricts().OrderBy(x => x.Name).Select(x => new CityDistrictDropDownItemDTO { Id = x.Id, Name = x.Name }).ToListAsync());
            searchParameters.RoomNumbers = new List<RoomNumberDownItemDTO>()
                { new RoomNumberDownItemDTO() { Id = null, Name = "No matter" }};
            searchParameters.RoomNumbers.AddRange(_realEstatesData.Rooms());

            searchParameters.SortOrders = _realeEstateSort.GetSortingOptionsName();
            return searchParameters;
        }

        public void Dispose()
        {
            _realEstatesData.Dispose();
        }
    }
}
