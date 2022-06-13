using Juan.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.ViewModels
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public int DiscountPercent { get; set; }
        public bool isDeleted { get; set; }
        public ICollection<ProductImages> Images { get; set; }

        [NotMapped, Required]
        public IFormFile Photo { get; set; }
    }
}
