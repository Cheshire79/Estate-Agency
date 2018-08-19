using System;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Realtor
{
    public class RealEstateToSaveView
    {
        [Required]
        [StringLength(7, MinimumLength = 1, ErrorMessage = "Building must be between 1 and 7 characters")]
        [RegularExpression(@"^[1-9]+\d*[\u0400-\u04FF]?(\/?[1-9]+\d*[\u0400-\u04FF]?)?", ErrorMessage = "Invalid Building value!")]
        public string Building { get; set; }

        [Required]
        [RegularExpression(@"^[1-9]+\d*",ErrorMessage= "Invalid Appartment value!")]
        public Int16 Appartment { get; set; }

        [Required]
        [RegularExpression(@"^[1-9]+\d*", ErrorMessage = "Invalid Floor value!")]
        public Int16 Floor { get; set; }

        [Required]
        [RegularExpression(@"^[1-9]+\d*", ErrorMessage = "Invalid Height value!")]
        public Int16 Height { get; set; }

        [Required]
        [RegularExpression(@"^[1-9]+\d*", ErrorMessage = "Invalid Area value!")]
        public Int16 Area { get; set; }

        [Required]
        [RegularExpression(@"^\d*\.?\d*", ErrorMessage = "Invalid Price value!")]
        public Decimal Price { get; set; }

        [Required]
        [RegularExpression(@"^[1-9]+\d*", ErrorMessage = "Invalid Room Number value!")]
        public byte RoomNumber { get; set; }

        public string Description { get; set; }
        public byte[] Image { get; set; }

        [RegularExpression(@"^[1-9]+\d*", ErrorMessage = "Streed need to be choosen!")]
        public int StreetId { get; set; }

        public int DistrictId { get; set; }
        public int Id { get; set; }
    }
}