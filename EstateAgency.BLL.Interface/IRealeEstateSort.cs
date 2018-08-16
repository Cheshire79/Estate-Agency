using System.Collections.Generic;
using System.Linq;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.ForManipulate;

namespace EstateAgency.BLL.Interface
{
    public delegate IQueryable<RealEstateForRealtorDTO> Sorting(IQueryable<RealEstateForRealtorDTO> realEstates);
    public interface IRealeEstateSort
    {
        List<SortOrderDropDownDTO> GetSortingOptionsName();
        Sorting Sort(SortOrder sortOrder);
    }
}
