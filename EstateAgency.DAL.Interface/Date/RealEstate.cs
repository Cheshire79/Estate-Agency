using System;
using System.ComponentModel.DataAnnotations;

namespace EstateAgency.DAL.Interface.Date
{
    public class RealEstate
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(7)]
        public string Building { get; set; }
        [Required]
        public Int16 Appartment { get; set; }
        [Required]
        public Int16 Floor { get; set; }
        [Required]
        public Int16 Height { get; set; }
        [Required]
        public Int16 Area { get; set; }
        [Required]
        public Decimal Price { get; set; }
        [Required]
        public byte RoomNumber { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

        public string Description { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public bool IsSold { get; set; }
        [Required]
        public int StreetId { get; set; }
        [Required]
        public string RealtorId { get; set; }
    }
}
