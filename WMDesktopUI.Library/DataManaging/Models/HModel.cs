﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WMDesktopUI.Library.DataManaging.Models
{
    
    public class HModel
    {
        public int Id { get; set; }
        public string FactoryNumber { get; set; }
        public string Name { get; set; }
        public string Set { get; set; }
        public string Type { get; set; }
        public byte[] Photo { get; set; }
        public string ProductDescription { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductNetPrice { get; set; }
        public decimal ProductSellPrice { get; set; }
        public int ClientId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime FinishDateTime { get; set; }
    }
}
