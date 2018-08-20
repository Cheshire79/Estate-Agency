using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EstateAgency.BLL.Interface;

using EstateAgency.BLL.Interface.Date.Client;
using EstateAgency.BLL.Interface.Date.ForManipulate;

namespace EstateAgency.BLL.Services
{
    public class ClientService : IClientService
    {
        private IRealeEstateSort<RealEstateForClientDTO> _realeEstateSort;
        private IRealEstatesDataMapper _realEstatesData;
        private IFilterForClient _filter;
        public ClientService( IRealeEstateSort<RealEstateForClientDTO> realeEstateSort,  IRealEstatesDataMapper realEstatesData, IFilterForClient filter)
        {
            _realeEstateSort = realeEstateSort;
            _realEstatesData = realEstatesData;
            _filter = filter;
        }

        public IQueryable<RealEstateForClientDTO> FormRealEstates(ChoosenSearchParametersForClientDTO parameters)
        {
            IQueryable<RealEstateForClientDTO> realEstates =
                from realEstate in _filter.FilteredRealEstates(_realEstatesData.RealEstates(), parameters)
                 join street in _realEstatesData.Streets() on realEstate.StreetId equals street.Id
                 join district in _filter.FilteredDistricts(_realEstatesData.KievDistricts(), parameters) on street.CityDistrictId equals district.Id
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
