using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Controllers
{
    public class CommentController : Controller
    {
        private readonly Context _context;
        public CommentController(Context context)
        {
            _context = context;
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Comments comment)
        {

            if (comment.Text == null || comment.Text.Length < 10)
                return RedirectToAction("detail", "blog", new { id = comment.BlogId });

            string userId = String.Empty;

            if (User.Identity.IsAuthenticated)
                userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            else
                return RedirectToAction("Login", "Account");


            bool isExist = _context.Comments.Any(c => c.BlogId == comment.BlogId && c.UserId == userId);
            if (isExist)
            {
                return RedirectToAction("detail", "blog", new { id = comment.BlogId });
            }

            try
            {
                Comments _comment = new Comments
                {
                    Text = comment.Text,
                    UserId = userId,
                    BlogId = comment.BlogId,
                    Date = DateTime.Now
                };

                await _context.Comments.AddAsync(_comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("detail", "blog", new { id = comment.BlogId });
            }
            catch
            {
                return View();
            }
        }

        // Get: CommentController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            string userId = String.Empty;

            if (User.Identity.IsAuthenticated)
                userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Comments comment = await _context.Comments.FindAsync(id);
            if (comment == null) return RedirectToAction("Index", "Home");

            try
            {
                if (comment.UserId == userId)
                {
                    _context.Comments.Remove(comment);
                    await _context.SaveChangesAsync();
                };

                return RedirectToAction("detail", "blog", new { id = comment.BlogId });
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
