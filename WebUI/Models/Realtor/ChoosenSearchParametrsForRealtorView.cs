using System;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Realtor
{
    public class ChoosenSearchParametrsForRealtorView
    {
        public int? DistrictId { get; set; } = null;
        public byte? RoomNumber { get; set; } = null;
        public int SortOrder { get; set; } = 1;

        [RegularExpression(@"^\d*")]
        public Int16? AreaFrom { get; set; }

        [RegularExpression(@"^\d*")]
        public Int16? AreaTo { get; set; }

        [RegularExpression(@"^\d*\.?\d*")]
        public decimal? PriceFrom { get; set; }

        [RegularExpression(@"^\d*\.?\d*")]
        public decimal? PriceTo { get; set; }

        [RegularExpression(@"^\d*")]
        public Int16? FloorFrom { get; set; }

        [RegularExpression(@"^\d*")]
        public Int16? FloorTo { get; set; }

        [RegularExpression(@"^\d*")]
        public Int16? HeightFrom { get; set; }

        [RegularExpression(@"^\d*")]
        public Int16? HeightTo { get; set; }

        public bool ShowOnlyMyOwn { get; set; }
        public int Page { get; set; } = 1;
    }

}