﻿using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Extensions;
using E_commerce_BackFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeProductSliderController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly Context _context;

        public HomeProductSliderController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }



        // GET: HomeProductSlidersController
        public ActionResult Index()
        {
            List<HomeProductSlider> slider = _context.homeProductSliders.Include(x => x.Product).ToList();
            return View(slider);
        }

        // GET: HomeProductSlidersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeProductSlidersController/Create
        public async Task<ActionResult> Create()
        {
            var products = await _context.Products
             .Include(x => x.productPhotos).ToListAsync();

            ViewBag.ProductList = products;

            return View();
        }

        // POST: HomeProductSlidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HomeProductSlider slider)
        {
            if (slider.Title.Length < 15 || slider.Description.Length < 20)
            {
                return View();
            }

            bool isExist = _context.Products.Any(x => x.Id == slider.ProductId);

            if (!isExist)
            {
                return View();
            }


            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Do not empty");
                return View();
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

            HomeProductSlider newSlider = new HomeProductSlider();
            newSlider.Title = slider.Title;
            newSlider.Description = slider.Description;
            newSlider.ProductId = slider.ProductId;
            string fileName = await slider.Photo.SaveImageAsync(_env.WebRootPath, "images/product/");
            newSlider.ImageUrl = fileName;

            await _context.homeProductSliders.AddAsync(newSlider);
            await _context.SaveChangesAsync();
            var products = await _context.Products
             .Include(x => x.productPhotos).ToListAsync();

            ViewBag.ProductList = products;

            return View();
        }

        // GET: HomeProductSlidersController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            HomeProductSlider slider = _context.homeProductSliders.Include(x => x.Product).FirstOrDefault(x => x.Id == id);
            var products = await _context.Products
             .Include(x => x.productPhotos).ToListAsync();

            ViewBag.ProductList = products;
            return View(slider);
        }

        // POST: HomeProductSlidersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HomeProductSlider slider)
        {
            if (slider.Title.Length < 15 || slider.Description.Length < 20)
            {
                return View();
            }

            bool isExist = _context.Products.Any(x => x.Id == slider.ProductId);

            if (!isExist)
            {
                return View();
            }

            HomeProductSlider newSlider = await _context.homeProductSliders.FirstOrDefaultAsync(x => x.Id == slider.Id);

            if (newSlider == null) return View();


            newSlider.Title = slider.Title;
            newSlider.Description = slider.Description;
            newSlider.ProductId = slider.ProductId;

            if (slider.Photo != null)
            {
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
                string fileName = await slider.Photo.SaveImageAsync(_env.WebRootPath, "images/product/");
                newSlider.ImageUrl = fileName;
            }

            await _context.SaveChangesAsync();


            return View();
        }

        // GET: HomeProductSlidersController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            HomeProductSlider slider = _context.homeProductSliders.Include(x => x.Product).FirstOrDefault(x => x.Id == id);
            var products = await _context.Products
             .Include(x => x.productPhotos).ToListAsync();

            ViewBag.ProductList = products;
            return View(slider);
        }

        // POST: HomeProductSlidersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(HomeProductSlider slider)
        {
            HomeProductSlider Deleteslider = _context.homeProductSliders.Include(x => x.Product).FirstOrDefault(x => x.Id == slider.Id);
            if (Deleteslider == null) return NotFound();

            _context.homeProductSliders.Remove(Deleteslider);
            await _context.SaveChangesAsync();
            return View();
        }
    }
}
