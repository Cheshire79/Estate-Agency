using System.Collections.Generic;

namespace WebUI.Models.Realtor.ForManipulate
{
    public class DataForManipulateRealEstateView
    {
        public List<CityDistrictDropItemView> Districts;
        public List<RoomNumberDropItemView> RoomNumbers;
        public List<StreetDropItemView> Streets;
        public int ChoosenDistrict;
        public byte ChoosenRoomNumber;
        public int ChoosenStreet;
        public string ReturnUrl { get; set; }
    }
}