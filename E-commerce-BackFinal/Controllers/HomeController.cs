﻿using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Models;
using E_commerce_BackFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;
        

        public HomeController(Context context)
        {
            _context = context;
        }
        

        public IActionResult Index()
        {
            List<CompanySlider> companySliders = _context.CompanySliders.ToList();
            List<Service> services = _context.Services.ToList();
            List<Category> categories = _context.Categories.Where(c=>c.IsMain==true).ToList();
            HomeVm homeVm = new HomeVm();
            homeVm.companySliders = companySliders;
            homeVm.services = services;
            homeVm.categories = categories;
            ViewBag.FeatCategories = categories.Where(c => c.IsFeature == true);
            ViewData["CompanySliders"] = companySliders;
            ViewData["Services"] = services;
            return View(homeVm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
