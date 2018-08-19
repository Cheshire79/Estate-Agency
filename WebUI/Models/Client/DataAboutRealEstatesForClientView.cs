using System.Collections.Generic;

namespace WebUI.Models.Client
{
    public class DataAboutRealEstatesForClientView
    {
        public ChoosenSearchParametrsForClientView ChoosenSearchParametersForRealtor;
        public DataForSearchParametersClientView SearchParameters;
        public List<RealEstateForClientView> RealEstates;
        public PagingInfo PagingInfo { get; set; }
    }
}