using System.Collections.Generic;
using WebUI.Models.Realtor.ForManipulate;

namespace WebUI.Models.Client
{
    public class DataForSearchParametersClientView
    {
        public List<CityDistrictDropItemView> Districts;
        public List<RoomNumberDropItemView> RoomNumbers;
        public List<SortOrderDropItemView> SortOrders;
    }
}