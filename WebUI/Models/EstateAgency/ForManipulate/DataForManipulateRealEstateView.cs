using System.Collections.Generic;

namespace WebUI.Models.EstateAgency.ForManipulate
{
    public class DataForManipulateRealEstateView
    {
        public List<CityDistrictDropItemView> Districts;
        public List<RoomNumberDropItemView> RoomNumbers;
        public List<StreetDropItemView> Streets;
        public int ChoosenDistrictId;
        public byte ChoosenRoomNumber;
        public int ChoosenStreetId;
        public string ReturnUrl { get; set; }
    }
}