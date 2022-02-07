﻿
using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Models;
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

    public class BrandController : Controller
    {
        private readonly Context _context;

        public BrandController(Context context)
        {
            _context = context;
        }
            // GET: BrandController
         public ActionResult Index()
        {
            List<Brand> brands = _context.Brands.Include(b=>b.CategoryBrand).ThenInclude(c=>c.Category).ToList();
            return View(brands);
        }

        // GET: BrandController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BrandController/Create
        public ActionResult Create()
        {
          ViewBag.IsMainCategory = _context.Categories.Where(c=>c.IsMain==true).ToList();
          ViewBag.SubCategory = _context.Categories.Where(c => c.IsMain == false).ToList();

            return View();
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Create(Brand brand,int[] subcategory)
        {
            bool isExist = _context.Brands.Any(c => c.Name.ToLower() == brand.Name.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Name", "This category is already exists");
                return RedirectToAction("Index");

            }
            if (subcategory != null)
            {
                Brand newBrand = new Brand();
                newBrand.Name = brand.Name;
                await _context.Brands.AddAsync(newBrand);
                await _context.SaveChangesAsync();

                foreach (var item in subcategory)
                {
                    CategoryBrand categoryBrands = new CategoryBrand();
                    categoryBrands.BrandId = newBrand.Id;
                    categoryBrands.CategoryId = item;
                    await _context.AddAsync(categoryBrands);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        // GET: BrandController/Edit/5
        public async Task<ActionResult> Edit(int?id)
        {
            //Brand brands =await _context.Brands.Include(b => b.CategoryBrand).ThenInclude(c => c.Category).FirstOrDefaultAsync(x=>x.Id==id);
            //ViewBag.SubCategory = await _context.Categories.Include(c=>c.CategoryBrand).ThenInclude(c=>c.Brand).Where(c => c.IsMain == false).ToListAsync();
            //ViewBag.IsMainCategory = _context.Categories.Where(c => c.IsMain == true).ToList();


            Brand brand = await _context.Brands.Include(b => b.CategoryBrand).ThenInclude(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);
            List<CategoryBrand> SubCategory = await _context.CategoryBrands.Include(c => c.Category).Where(x => x.BrandId == brand.Id).ToListAsync();
            
            List<Category> AllCategory = await _context.Categories.Include(c => c.CategoryBrand).ThenInclude(c => c.Brand).Where(c=>c.IsMain==false).ToListAsync();
            foreach (var item in SubCategory)
            {
                AllCategory.Remove(item.Category);

            }
            ViewBag.checkCategory = SubCategory;
            ViewBag.noneCheck = AllCategory;
            return View(brand);
        }

        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int?id, Brand brand, int[] subcategory)
        {
            bool isExist = _context.Brands.Any(c => c.Name.ToLower() == brand.Name.ToLower().Trim());
            Brand newBrand = await _context.Brands.FindAsync(id);

            if (isExist && !(newBrand.Name.ToLower() == brand.Name.ToLower().Trim()))
            {


                ModelState.AddModelError("Name", "Bu adla Brand var");
                return RedirectToAction("Index");
            }
           var allSubCategory = _context.Categories.Where(c => c.IsMain == false).ToList();
            var checkedCategory= _context.CategoryBrands.Where(c => c.BrandId == newBrand.Id).ToList();
            
            List<int> allCategoryList = new List<int>();
            List<int> checkedCategoryList = new List<int>();

            foreach (var item in allSubCategory)
            {
                allCategoryList.Add(item.Id);
            }

            foreach (var item in checkedCategory)
            {
                checkedCategoryList.Add(item.CategoryId);
            }
            
            var addedCategory = subcategory.Except(checkedCategoryList);
            var removedCategory = checkedCategoryList.Except(subcategory);

            newBrand.Name = brand.Name;


            foreach (var item in removedCategory)
            {
                CategoryBrand categoryBrand = await _context.CategoryBrands.Where(c => c.CategoryId == item && c.BrandId == newBrand.Id).FirstOrDefaultAsync();
                _context.CategoryBrands.Remove(categoryBrand);
                await _context.SaveChangesAsync();
            }
            if (addedCategory != null)
            {
                foreach (var item in addedCategory)
                {
                    Category category = _context.Categories.Find(item);
                    CategoryBrand categoryBrand = new CategoryBrand();
                    categoryBrand.BrandId = newBrand.Id;
                    categoryBrand.CategoryId = category.Id;
                    await _context.CategoryBrands.AddAsync(categoryBrand);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            return NotFound();



        }

        // GET: BrandController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BrandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
