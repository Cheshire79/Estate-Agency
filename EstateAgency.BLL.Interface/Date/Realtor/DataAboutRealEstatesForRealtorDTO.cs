using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateAgency.BLL.Interface.Date.Realtor
{
	public class DataAboutRealEstatesForRealtorDTO
	{
		public ChoosenSearchParametersForRealtorDTO ChoosenSearchParametersForRealtor;		       
		public DataForSearchParametersRealtorDTO SearchParameters;
		public List<RealEstateForRealtorDTO> RealEstates;
		public PagingInfoDTO PagingInfo { get; set; }
	}
}
