using System.Collections.Generic;

namespace WebUI.Models.EstateAgency.Client
{
    public class DataAboutRealEstatesForClientView
    {
        public ChoosenSearchParametrsForClientView ChoosenSearchParametersForRealtor;
        public DataForSearchParametersClientView SearchParameters;
        public List<RealEstateForClientView> RealEstates;
        public PagingInfoView PagingInfo { get; set; }
    }
}