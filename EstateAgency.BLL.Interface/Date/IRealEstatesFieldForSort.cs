using System;

namespace EstateAgency.BLL.Interface.Date
{
    public interface IRealEstateFieldsForSort
    {
        Int16 Area { get; set; }
        Decimal Price { get; set; }
        DateTime CreationDate { get; set; }
    }
}
