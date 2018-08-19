using System.Linq;
using EstateAgency.BLL.Interface.Date.Realtor;

namespace EstateAgency.BLL.Interface.Date
{
    public abstract class RealeEstateOrder
    {
        public abstract IQueryable<RealEstateForRealtorDTO> Order(IQueryable<RealEstateForRealtorDTO> realEstates);
    }
}

