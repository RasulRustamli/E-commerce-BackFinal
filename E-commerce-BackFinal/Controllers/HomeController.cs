using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Models;
using E_commerce_BackFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            
            List<Category> categories = _context.Categories.Where(c=>c.IsMain==true).ToList();
            List<Product> products = _context.Products.Include(p => p.Campaign).Include(p => p.productPhotos).Include(p => p.Brand).ToList();

            HomeVm homeVm = new HomeVm();

            ViewBag.newarrive = products.OrderByDescending(p => p.Id).Take(7).ToList();

            homeVm.categories = categories;
            homeVm.products = products;
            ViewBag.FeatCategories = categories.Where(c => c.IsFeature == true);
           
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
