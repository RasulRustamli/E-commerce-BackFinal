using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

    public class MessageController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        public MessageController(Context context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
           
        }
        // GET: MessageController
        public ActionResult Index()
        {
            List<Message> messages = _context.Messages.Include(u => u.User).ToList();
            
            return View(messages);
        }

        // GET: MessageController/Details/5
       
        public async Task<ActionResult> Details(int?id)
        {
            if (id == null) return NotFound();
            List<Message> messages = _context.Messages.Include(u => u.User).ToList();
            var singlemessages = messages.Find(x =>x.Id == id);
            if (singlemessages == null) return NotFound();
            AppUser user = await _userManager.FindByIdAsync(singlemessages.UserId);
            singlemessages.User = user;
            return View(singlemessages);
        }

       

        // GET: MessageController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MessageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: MessageController/Delete/5
        public async Task<ActionResult> Delete(int?id)
        {
            if (id == null) return NotFound();
            List<Message> messages = _context.Messages.Include(u => u.User).ToList();
            var singlemessages = messages.Find(x => x.Id == id);
            if (singlemessages == null) return NotFound();
            AppUser user = await _userManager.FindByIdAsync(singlemessages.UserId);
            singlemessages.User = user;
            return View(singlemessages);
        }

        //POST: MessageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Message message)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
