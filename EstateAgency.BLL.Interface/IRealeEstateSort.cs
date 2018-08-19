using System.Collections.Generic;
using System.Linq;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.ForManipulate;

namespace EstateAgency.BLL.Interface
{
    public delegate IQueryable<T> Sorting<T>(IQueryable<T> realEstates);
    public interface IRealeEstateSort<T> where T : class, IRealEstateFieldsForSort
    {
        List<SortOrderDropDownDTO> GetSortingOptionsName();
        Sorting<T> Sort(SortOrder sortOrder);
    }
}
