using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.ForManipulate;
using EstateAgency.BLL.Interface.Date.Realtor;

namespace EstateAgency.BLL.Interface
{
    public interface IRealtorService : IDisposable
    {
        Task SetInitialData(string realtorId);
        Task<List<StreetDropDownItemDTO>> GetStreetsForDropDownByDistrctId(int districtId);
        Task Create(RealEstateDTO realEstateDTO,string realtorId);
        Task Save(RealEstateDTO realEstateDTO);
		Task<DataAboutRealEstatesForRealtorDTO>  FormRealEstates(string userId, ChoosenSearchParametersForRealtorDTO parameters);
        Task<EditRealEstateDTO> GetDataForRealEstateEditing(int id, string userId);
        Task<DataForSearchParametersRealtorDTO> InitiateSearchParameters();
        Task<DataForManipulateRealEstateDTO> GetDataForRealEstateManipulate(int? specifiedDistrictId = null, int? specifiedStreetId = null, byte? specifiedRoomNumber=null);
		Task MarkRealEstateAsSold(int realEstateId);
		Task DeleteRealEstate(int realEstateId);
	}
}
