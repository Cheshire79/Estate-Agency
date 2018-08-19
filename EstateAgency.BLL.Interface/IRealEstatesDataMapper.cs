using System;
using System.Collections.Generic;
using System.Linq;
using EstateAgency.BLL.Interface.Date;
using EstateAgency.BLL.Interface.Date.ForManipulate;

namespace EstateAgency.BLL.Interface
{
    public interface IRealEstatesDataMapper : IDisposable
    {
        IQueryable<CityDistrictDTO> KievDistricts();
        IQueryable<RealEstateDTO> RealEstates();
        IQueryable<StreetDTO> Streets();
        List<RoomNumberDownItemDTO> Rooms();

    }
}
