﻿using System;

namespace WebUI.Models.EstateAgency.Client
{
    public class RealEstateForClientView
    {
        public int Id { get; set; }
        public string Building { get; set; }
        public Int16 Floor { get; set; }
        public Int16 Height { get; set; }
        public Int16 Area { get; set; }
        public Decimal Price { get; set; }
        public byte RoomNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int StreetId { get; set; }
        public string StreetName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string RealtorId { get; set; }
        public string RealtorName { get; set; }
        public string RealtorEmail { get; set; }
    }
}