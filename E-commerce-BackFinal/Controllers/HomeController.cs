using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Models;
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
            return View(companySliders);
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
