using System.Collections.Generic;

namespace WebUI.Models.Realtor
{
    public class DataAboutRealEstatesForRealtorView
    {
        public ChoosenSearchParametrsForRealtorView ChoosenSearchParametersForRealtor;
        public DataForSearchParametersRealtorView SearchParameters;
        public List<RealEstateForRealtorView> RealEstates;
        public PagingInfo PagingInfo { get; set; }
    }
}