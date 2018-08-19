using System.Collections.Generic;
using EstateAgency.BLL.Interface.Date.ForManipulate;

namespace EstateAgency.BLL.Interface.Date.Realtor
{
    public class DataForSearchParametersRealtorDTO
    {
        public List<CityDistrictDropDownItemDTO> Districts;
        public List<RoomNumberDownItemDTO> RoomNumbers;
        public List<SortOrderDropDownDTO> SortOrders;
    }
}

