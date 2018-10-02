using System.Collections.Generic;
using WebUI.Models.Realtor;

namespace WebUI.Models.EstateAgency.Realtor
{
    public class DataAboutRealEstatesForRealtorView
    {
        public ChoosenSearchParametersForRealtorView ChoosenSearchParametersForRealtor;
        public DataForSearchParametersRealtorView SearchParameters;
        public List<RealEstateForRealtorView> RealEstates;
        public PagingInfoView PagingInfo { get; set; }
    }
}