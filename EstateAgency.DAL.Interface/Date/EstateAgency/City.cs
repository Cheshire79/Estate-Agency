using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EstateAgency.DAL.Interface.Date
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public ICollection<CityDistrict> CityDistricts { get; set; }

        public City()
        {
            CityDistricts = new List<CityDistrict>();
        }
    }
}
