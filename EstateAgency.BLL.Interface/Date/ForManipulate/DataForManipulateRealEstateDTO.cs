using System.Collections.Generic;

namespace EstateAgency.BLL.Interface.Date.ForManipulate
{
    public class DataForManipulateRealEstateDTO
    {
        public List<CityDistrictDropDownItemDTO> Districts;
        public List<RoomNumberDownItemDTO> RoomNumbers;
        public List<StreetDropDownItemDTO> Streets;
        public int? ChoosenDistrictId;
        public byte ChoosenRoomNumber;
        public int? ChoosenStreetId;
    }
}
