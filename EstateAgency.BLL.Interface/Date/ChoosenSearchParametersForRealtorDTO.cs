
using System;

namespace EstateAgency.BLL.Interface.Date
{
    public class ChoosenSearchParametersForRealtorDTO
    {
        public int? DistrictId = null;
        public byte? RoomNumber = null;
        public SortOrder SortOrder;

        public Int16? AreaFrom;
        public Int16? AreaTo;

        public decimal? PriceFrom;
        public decimal? PriceTo;

        public Int16? FloorFrom;
        public Int16? FloorTo;

        public Int16? HeightFrom;
        public Int16? HeightTo;
        public bool ShowOnlyMyOwn = false;
        public int? Page;
    }
}
