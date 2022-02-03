using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Controllers
{
    public class ContactController : Controller
    {
        private readonly Context _context;


        public ContactController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ContactUS contact = _context.Contacts.FirstOrDefault();
            List<CompanySlider> companySliders = _context.CompanySliders.ToList();
            List<Service> services = _context.Services.ToList();
            ViewData["CompanySliders"] = companySliders;
            ViewData["Services"] = services;
            return View(contact);
        }
    }
}
