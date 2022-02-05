using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Extensions;
using E_commerce_BackFinal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
        public class SubscribeController : Controller
    {
            private readonly IWebHostEnvironment _env;
            private readonly Context _context;

            public SubscribeController(Context context, IWebHostEnvironment env)
            {
                _context = context;
                _env = env;
            }
            // GET: SubscribeController
            public ActionResult Index()
        {
            Subscribe subscribe = _context.Subcribes.FirstOrDefault();
            return View(subscribe);
        }

        // GET: SubscribeController/Details/5
        public async Task<ActionResult> Details(int?id)
        {
            if (id == null) return NotFound();
            Subscribe dbSub = await _context.Subcribes.FindAsync(id);
            if (dbSub == null) return NotFound();
            return View(dbSub);

        }

        // GET: SubscribeController/Edit/5
        public async Task<ActionResult> Edit(int?id)
        {
            if (id == null) return NotFound();
            Subscribe dbSub = await _context.Subcribes.FindAsync(id);
            if (dbSub == null) return NotFound();
            return View(dbSub);
        }

        // POST: SubscribeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Subscribe subscribe)
        {

            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
            }

            if (!subscribe.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (subscribe.Photo.IsCorrectSize(300))
            {
                ModelState.AddModelError("Photo", "please enter photo under 300kb");
                return View();
            }

            Subscribe newSub = new Subscribe();

            string fileName = await subscribe.Photo.SaveImageAsync(_env.WebRootPath, "images/subscribe/");
            newSub.PhotoUrl = fileName;
            newSub.Title = subscribe.Title;
            newSub.Description = subscribe.Description;
            

            await _context.Subcribes.AddAsync(newSub);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }

        // GET: SubscribeController/Delete/5
    }
}
