using Juan.DAL;
using Juan.Helpers;
using Juan.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private AppDbContext _context { get; }
        private IWebHostEnvironment _env { get; }
        public SliderController(AppDbContext context, IWebHostEnvironment env )
        {
            _context = context;
            _env = env;
        }                       
        public IActionResult Index()
        {
            return View(_context.Slider);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id,JuanSlider slide)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!slide.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "max size must be less than 200kb");
                return View();
            }
            slide.Image = await slide.Photo.SaveFileAsync(_env.WebRootPath, "assets/img/slider");
            await _context.Slider.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var slideDb = _context.Slider.Find(id);
            if (slideDb == null)
            {
                return NotFound();
            }
            var path = Helper.GetPath(_env.WebRootPath, "img", slideDb.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Slider.Remove(slideDb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var slider = _context.Slider.Find(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, JuanSlider slider)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var sliderDb = _context.Slider.Find(id);
            if (sliderDb == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!slider.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Maximum file size is 200 Kb!");
                return View();
            }
            if (!slider.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File type must be image");
                return View();
            }
            slider.Image = await slider.Photo.SaveFileAsync(_env.WebRootPath, "assets/img/slider");
            var path = Helper.GetPath(_env.WebRootPath, "img", sliderDb.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            sliderDb.Image = slider.Image;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
