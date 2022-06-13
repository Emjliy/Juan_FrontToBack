using Juan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.ViewModels
{
    public class HomeViewModel
    {
        public List<JuanSlider> Slider { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductImages> ProductImages { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Brand> Brands { get; set; }

    }
}
