using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Controllers
{
    public class BlogController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public BlogController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var blog =  _context.Blogs.ToList();
            var photos = _context.BlogPhotos.ToList();
            ViewBag.photos = photos;
            return View(blog);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            var blog = await _context.Blogs.Include(x => x.BlogPhotos).FirstOrDefaultAsync(x => x.Id == id);
            var user = await _userManager.FindByIdAsync(blog.UserId);
            var tags = await _context.ProductTags.Where(p => p.ProductId == blog.ProductId).Select(t=>t.Tag).ToListAsync();
            ViewBag.user = user.FullName;
            ViewBag.tags = tags;
            return View(blog);
        }
    }
}
