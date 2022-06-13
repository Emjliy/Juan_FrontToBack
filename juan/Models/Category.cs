using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isDeleted { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
