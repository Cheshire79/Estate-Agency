using System;
using System.Linq;
using System.Threading.Tasks;
using EstateAgency.BLL.Interface.Date.Client;

namespace EstateAgency.BLL.Interface
{
    public interface IClientService : IDisposable
    {
        IQueryable<RealEstateForClientDTO> FormRealEstates(ChoosenSearchParametersForClientDTO parameters);
        Task<DataForSearchParametersClientDTO> InitiateSearchParameters();
    }
}
