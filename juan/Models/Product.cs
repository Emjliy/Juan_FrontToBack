using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public int DiscountPercent { get; set; }
        public bool isDeleted { get; set; }
        public bool isNew { get; set; }
        public bool isInStock { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public ICollection<ProductImages> Images { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        
    }
}
