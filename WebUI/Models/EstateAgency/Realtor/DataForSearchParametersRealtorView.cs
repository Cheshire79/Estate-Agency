using System.Collections.Generic;
using WebUI.Models.EstateAgency.ForManipulate;

namespace WebUI.Models.EstateAgency.Realtor
{
    public class DataForSearchParametersRealtorView
    {
        public List<CityDistrictDropItemView> Districts;
        public List<RoomNumberDropItemView> RoomNumbers;
        public List<SortOrderDropItemView> SortOrders;
    }
}