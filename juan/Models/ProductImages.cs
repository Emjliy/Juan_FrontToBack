using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.Models
{
    public class ProductImages
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string Image { get; set; }
        public bool isMain { get; set; }
        public Product Products { get; set; }
    }
}
