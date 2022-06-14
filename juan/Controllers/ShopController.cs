using Juan.DAL;
using Juan.Models;
using Juan.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.Controllers
{
    public class ShopController : Controller
    {
        private AppDbContext _context { get; }
        private IWebHostEnvironment _env { get; }
        public ShopController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            ShopViewModel shop = new ShopViewModel
            {
                Products = _context.Products.Include(p => p.Images).Include(p => p.ProductCategories).ThenInclude(pc => pc.Categories)
                .Where(p => !p.isDeleted && p.Images.Any(pi =>!pi.isMain)).Take(9).ToList(),
            };
            return View(shop);
        }
        public IActionResult Detail(int? id)
        {
            return View();
        }
    }
    }
