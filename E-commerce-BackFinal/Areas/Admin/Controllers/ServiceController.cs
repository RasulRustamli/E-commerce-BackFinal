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
    public class ServiceController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly Context _context;

        public ServiceController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // GET: ServiceController
        public ActionResult Index()
        {
            List<Service> services = _context.Services.ToList();

            return View(services);
        }

        // GET: ServiceController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            Service dbService = await _context.Services.FindAsync(id);
            if (dbService == null) return NotFound();
            return View(dbService);
        }

        // GET: ServiceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Service service )
        {
            if (!ModelState.IsValid) return View();
            bool isExist = _context.Services.Any(c => c.Title.ToLower() == service.Title.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu basliqda servis movcuddur");
                return View();
            }
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!service.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (service.Photo.IsCorrectSize(300))
            {
                ModelState.AddModelError("Photo", "please enter photo under 300kb");
                return View();
            }

            Service newService = new Service();

            string fileName = await service.Photo.SaveImageAsync(_env.WebRootPath, "images/service-icon/");
            newService.IconUrl = fileName;
            newService.Title = service.Title;
            newService.Description = service.Description;

            await _context.Services.AddAsync(newService);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");

        }

        // GET: ServiceController/Edit/5
        public async Task<ActionResult> Edit(int?id)
        {
            if (id == null) return NotFound();
            Service dbService = await _context.Services.FindAsync(id);
            if (dbService == null) return NotFound();
            return View(dbService);
            
        }

        // POST: ServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int?id, Service service)
        {
            if (id == null) return NotFound();
            bool isExist = _context.Services.Any(c => c.Title.ToLower() == service.Title.ToLower().Trim());

            Service isExistService = _context.Services.FirstOrDefault(c => c.Id == service.Id);

            if (isExist && !(isExistService.Title.ToLower() == service.Title.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "Bu adla Servis movcuddur");
                return View();
            };
            if (service.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    ModelState.AddModelError("Photo", "Do not empty");
                }

                if (!service.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "only image");
                    return View();
                }
                if (service.Photo.IsCorrectSize(300))
                {
                    ModelState.AddModelError("Photo", "300den yuxari ola bilmez");
                    return View();
                }
                string path = Path.Combine(_env.WebRootPath, "images", isExistService.IconUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string fileName = await service.Photo.SaveImageAsync(_env.WebRootPath, "images/service-icon");
                isExistService.IconUrl = fileName;
                isExistService.Title = service.Title;
                isExistService.Description = service.Description;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // GET: ServiceController/Delete/5
        public async Task<ActionResult>Delete(int?id)
        {
            if (id == null) return NotFound();
            Service dbService = await _context.Services.FindAsync(id);
            if (dbService == null) return NotFound();
            return View(dbService);
        }

        // POST: ServiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Service service)
        {
            Service dbService = await _context.Services.FindAsync(id);
            if (dbService == null) return NotFound();
            string path = Path.Combine(_env.WebRootPath, "images/service-icon/",dbService.IconUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Services.Remove(dbService);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
