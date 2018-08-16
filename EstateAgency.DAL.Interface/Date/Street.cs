using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EstateAgency.DAL.Interface.Date
{
    public class Street
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int CityDistrictId { get; set; }
        public ICollection<RealEstate> RealEstates { get; set; }
        public Street()
        {
            RealEstates = new List<RealEstate>();
        }
    }
}
