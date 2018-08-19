using System.Collections.Generic;
using WebUI.Models.EstateAgency.ForManipulate;

namespace WebUI.Models.EstateAgency.Client
{
    public class DataForSearchParametersClientView
    {
        public List<CityDistrictDropItemView> Districts;
        public List<RoomNumberDropItemView> RoomNumbers;
        public List<SortOrderDropItemView> SortOrders;
    }
}