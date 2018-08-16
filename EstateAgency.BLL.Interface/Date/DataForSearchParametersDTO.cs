using System.Collections.Generic;
using EstateAgency.BLL.Interface.Date.ForManipulate;

namespace EstateAgency.BLL.Interface.Date
{
    public class DataForSearchParametersDTO
    {
        public List<CityDistrictDropDownItemDTO> Districts;
        public List<RoomNumberDownItemDTO> RoomNumbers;
        public List<SortOrderDropDownDTO> SortOrders;
    }
}

