﻿using System.Collections.Generic;

namespace SalesStatistics.ModelLayer.Models
{
    public sealed class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        
        public ICollection<Sale> Sales { get; set; }
        
        public Product()
        {
            Sales = new HashSet<Sale>();
        }
    }
}