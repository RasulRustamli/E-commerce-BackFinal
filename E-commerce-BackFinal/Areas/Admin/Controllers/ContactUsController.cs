using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class ContactUsController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly Context _context;

        public ContactUsController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // GET: ContactUsController
        public ActionResult Index()
        {
            List<ContactUS> contactUS = _context.Contacts.ToList();
            return View(contactUS);
            
        }

        // GET: ContactUsController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            ContactUS dbContact = await _context.Contacts.FindAsync(id);
            if (dbContact == null) return NotFound();
            return View(dbContact);
        }

        // GET: ContactUsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactUsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ContactUS contact)
        {
            ContactUS newContact = new ContactUS
            {
                Description=contact.Description,
                Address=contact.Address,
                MapUrl=contact.MapUrl,
                Email=contact.Email,
                SupportMail=contact.SupportMail,
                MobileNumber=contact.MobileNumber,
                HotlineNumber=contact.HotlineNumber,
                OpenClose=contact.OpenClose,
            };

            await _context.AddAsync(newContact);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: ContactUsController/Edit/5
        public async Task<ActionResult> Edit(int?id)
        {
            if (id == null) return NotFound();
            ContactUS dbContact = await _context.Contacts.FindAsync(id);
            if (dbContact == null) return NotFound();
            return View(dbContact);
        }

        // POST: ContactUsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ContactUS contact)
        {
            ContactUS dbContact = await _context.Contacts.FindAsync(id);
            dbContact.HotlineNumber = contact.HotlineNumber;
            dbContact.MapUrl = contact.MapUrl;
            dbContact.MobileNumber = contact.MobileNumber;
            dbContact.OpenClose = contact.OpenClose;
            dbContact.SupportMail = contact.SupportMail;
            dbContact.Email = contact.Email;
            dbContact.Description = contact.Description;
            dbContact.Address = contact.Address;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        // GET: ContactUsController/Delete/5
        public async Task<ActionResult> Delete(int?id)
        {
            if (id == null) return NotFound();
            ContactUS dbContact = await _context.Contacts.FindAsync(id);
            if (dbContact == null) return NotFound();
            return View(dbContact);
            
        }

        // POST: ContactUsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Delete(int id)
        {
            ContactUS dbContact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(dbContact);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
