using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Controllers
{
    public class AboutController : Controller
    {
        private readonly Context _context;

        public AboutController(Context context)
        {
            _context = context;
        }

        // GET: About
        public ActionResult Index()
        {
            About about = _context.Abouts.FirstOrDefault();
            return View(about);
        }
    }
}
