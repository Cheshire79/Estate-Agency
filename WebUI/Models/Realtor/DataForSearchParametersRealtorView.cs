using System.Collections.Generic;
using WebUI.Models.Realtor.ForManipulate;

namespace WebUI.Models.Realtor
{
    public class DataForSearchParametersRealtorView
    {
        public List<CityDistrictDropItemView> Districts;
        public List<RoomNumberDropItemView> RoomNumbers;
        public List<SortOrderDropItemView> SortOrders;
    }
}