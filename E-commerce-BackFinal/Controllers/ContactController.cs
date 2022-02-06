using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Models;
using E_commerce_BackFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Controllers
{
    public class ContactController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public ContactController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ContactUS contact = _context.Contacts.FirstOrDefault();
            List<CompanySlider> companySliders = _context.CompanySliders.ToList();
            List<Service> services = _context.Services.ToList();
            ViewData["CompanySliders"] = companySliders;
            ViewData["Services"] = services;
            ContactUsVm contactUsVm = new ContactUsVm();
            if (User.Identity.IsAuthenticated)
            {
                contactUsVm.User = await _userManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            }
            contactUsVm.Contact = contact;
            return View(contactUsVm);
        }
        
        [HttpPost]
        public async Task<IActionResult> Message([FromForm]Message message)
        {
            if (User.Identity.IsAuthenticated)
            {
                Message newMessage = new Message();

                newMessage.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                newMessage.Title = message.Title;
                newMessage.Description = message.Description;
                await _context.Messages.AddAsync(newMessage);

                _context.SaveChanges();

            }

            else
            {
                return RedirectToAction("LogIn", "Account");
            }
                
            

            return Ok(new { Message="Success your send message"});
        }
    }
}
