using Juan.DAL;
using Juan.Helpers;
using Juan.Models;
using Juan.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ProductController : Controller
    {
        private AppDbContext _context { get; }
        private IWebHostEnvironment _env { get; }
        private IEnumerable<Product> products;
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            //productsImg = _context.Images.Where(pi => pi.isMain).ToList();
            //products = _context.Products.Include(p => p.Images).Include(p => p.ProductCategories).ThenInclude(pc => pc.Categories)
            //  .Where(p => !p.isDeleted && p.Images.Any(pi => pi.isMain)).ToList();
            products = _context.Products.Include(p => p.Images).Where(p => !p.isDeleted && p.Images.Any(pi => !pi.isMain)).Take(9).ToList();
        }
        public IActionResult Index()
        {
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM product)
        {
            //if (product.Photo == null)
            //{
            //    return NotFound();
            //}
             if (!ModelState.IsValid)
            {
                return View();
            }
            if (!product.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "max size must be  less than 200kb");
                return View();
            }
            if (!product.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "type file must be image");
                return View();
            }
            Product newProduct = new Product
            {
                //Images = product.Images,
                Name = product.Name,
                DiscountPercent = product.DiscountPercent
            };

            string image = await product.Photo.SaveFileAsync(_env.WebRootPath, "assets/img/product");

            ProductImages productImages = new ProductImages
            {
                Image = image
            };
            List<ProductImages> images = new List<ProductImages>();
            images.Add(productImages);

            newProduct.Images = images;

            await _context.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
          
        }
      
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return BadRequest();
        //    }
        //    var dbProduct = _context.Products.Find(id);
        //    if (dbProduct == null)
        //    {
        //        return NotFound();
        //    }
        //    var path = Helper.GetPath(_env.WebRootPath, "img", dbProduct.Images.FirstOrDefault().Image);
        //    if (System.IO.File.Exists(path))
        //    {
        //        System.IO.File.Delete(path);
        //    }
        //    _context.Products.Remove(dbProduct);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));

        //}
    }
}
