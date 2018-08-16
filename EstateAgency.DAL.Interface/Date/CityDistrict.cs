using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EstateAgency.DAL.Interface.Date
{
    public class CityDistrict
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public int CityId { get; set; }
        public ICollection<Street> Streets { get; set; }

        public CityDistrict()
        {
            Streets = new List<Street>();
        }
    }
}
