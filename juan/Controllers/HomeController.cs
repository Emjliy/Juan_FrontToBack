using Juan.DAL;
using Juan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context { get; }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel home = new HomeViewModel
            {
                Slider = _context.Slider.ToList(),
                Categories = _context.Categories.Where(c => !c.isDeleted).ToList(),
                Products = _context.Products.Include(p => p.Images).Include(p => p.ProductCategories).ThenInclude(pc => pc.Categories)
                .Where(p => !p.isDeleted && p.Images.Any(pi =>!pi.isMain)).Take(6).ToList(),
                //Categories = _context.Categories.Where(c => !c.isDeleted)
                //.Include(pc => pc.ProductCategories).ThenInclude(ct => ct.Products).ToList(),
                Blogs = _context.Blogs.ToList(),
                Brands = _context.Brands.ToList(),

            };
            return View(home);
        }
    }
}
