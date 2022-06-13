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

namespace Juan.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private AppDbContext _context { get; }
        private IEnumerable<Category> categories;
        public CategoryController(AppDbContext context)
        {
            _context = context;
            categories = _context.Categories.Where(ct => !ct.isDeleted);
        }
        public IActionResult Index()
        {
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id,CategoryCreateVM category)
        {

            if (!ModelState.IsValid) return View();
            bool isExist = false;
            foreach (var ct in categories)
            {
                if (category.Name.ToLower() == ct.Name.ToLower())
                {
                    isExist = true; break;
                }
            }
            if (isExist)
            {
                ModelState.AddModelError("NAME", $"{category.Name} is exist.");
                return View();
            }
            Category newCategory = new Category
            {
                Name = category.Name
            };
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory == null)
            {
                return NotFound();
            }
            return View(dbCategory);    
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,Category category)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Category dbCategory = _context.Categories.Where(c => !c.isDeleted).FirstOrDefault(c => c.ID == id);
            if (dbCategory == null)
            {
                return NotFound();
            }
            //eyni addisa databaseye sorgu getmir 
            if (category.Name.ToLower() == dbCategory.Name.ToLower())
            {
                return RedirectToAction(nameof(Index));
            }
            //deishdiyimiz adda olub olmadigini yoxlayir
            bool isExist = categories.Where(c => !c.isDeleted).Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("NAME", $"{category.Name} is exist.");
                return View();
            }
            dbCategory.Name = category.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dbCategory = await _context.Categories.FirstOrDefaultAsync(m => m.ID == id);
            if (dbCategory == null)
            {
                return NotFound();
            }
            dbCategory.isDeleted = true;
            await _context.SaveChangesAsync();
            return View(dbCategory);
        }
    }
}
