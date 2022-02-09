using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Extensions;
using E_commerce_BackFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CompanySliderController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly Context _context;

        public CompanySliderController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // GET: CompanySliderController
        public ActionResult Index()
        {
            List<CompanySlider> companySliders = _context.CompanySliders.ToList();
            return View(companySliders);
        }

        // GET: CompanySliderController/Create
        public ActionResult Create()
        {

            return View();
        }

        //POST: CompanySliderController/Create
       [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanySlider slider)
        {
            
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (slider.Photo.IsCorrectSize(300))
            {
                ModelState.AddModelError("Photo", "please enter photo under 300kb");
                return View();
            }

            CompanySlider newSlider = new CompanySlider();

            string fileName = await slider.Photo.SaveImageAsync(_env.WebRootPath, "images/company/");
            newSlider.PhotoUrl = fileName;
            newSlider.Name = slider.Name;

            await _context.CompanySliders.AddAsync(newSlider);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }


        // GET: CompanySliderController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            CompanySlider dbCompany = await _context.CompanySliders.FindAsync(id);
            if (dbCompany == null) return NotFound();
            return View(dbCompany);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CompanySlider slider)
        {
            if (id == null) return NotFound();
            bool isExist = _context.CompanySliders.Any(c => c.Name.ToLower() == slider.Name.ToLower().Trim());

            CompanySlider isExistCompany = _context.CompanySliders.FirstOrDefault(c => c.Id == slider.Id);

            if (isExist && !(isExistCompany.Name.ToLower() == slider.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "Bu adla Company var");
                return View();
            };
            if (slider.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    ModelState.AddModelError("Photo", "Do not empty");
                }

                if (!slider.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "only image");
                    return View();
                }
                if (slider.Photo.IsCorrectSize(300))
                {
                    ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                    return View();
                }
                string path = Path.Combine(_env.WebRootPath, "img", isExistCompany.PhotoUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string fileName = await slider.Photo.SaveImageAsync(_env.WebRootPath, "images/company");
                isExistCompany.PhotoUrl = fileName;
                isExistCompany.Name = slider.Name;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // GET: CompanySliderController/Delete/5
        public async Task<ActionResult> Delete(int?id)
        {
            if (id == null) return NotFound();
            CompanySlider dbSlider = await _context.CompanySliders.FindAsync(id);
            if (dbSlider == null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "images/company/", dbSlider.PhotoUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.CompanySliders.Remove(dbSlider);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        
        
    }
}
