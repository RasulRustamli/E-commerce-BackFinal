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
            var blog = _context.Blogs.ToList();
            var photos = _context.BlogPhotos.ToList();
            ViewBag.photos = photos;

            //RECENT POSTS
            var recentPosts = _context.Blogs
                .Take(4)
                .ToList();

            ViewBag.RecentPosts = recentPosts;

            return View(blog);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            List<Comments> comments = await _context.Comments
                .Where(c => c.BlogId == id)
                .Include(x => x.User).ToListAsync();

            var blog = await _context.Blogs
                .Include(b => b.BlogPhotos)
                .FirstOrDefaultAsync(b => b.Id == id);

            var user = await _userManager.FindByIdAsync(blog.UserId);
            var tags = await _context.ProductTags
                .Where(p => p.ProductId == blog.ProductId)
                .Select(t => t.Tag)
                .ToListAsync();

            var photos = _context.BlogPhotos.ToList();

            //RECENT POSTS
            var recentPosts = _context.Blogs
                .Take(4)
                .ToList();

            // RELATED POSTS
            var product = _context.ProductTags.Include(p => p.Tag).ToList();
            var relatedPosts = _context.Blogs
                .Include(b => b.BlogPhotos)
                .Include(b => b.Product)
                .ThenInclude(p => p.ProductTags)
                .ThenInclude(r => r.Tag)
                .ToList();

            ViewBag.relatedPosts = relatedPosts.Where(r => r.Product.ProductTags[0].TagId == product[0].TagId);

            ViewBag.photos = photos;
            ViewBag.RecentPosts = recentPosts;
            ViewBag.user = user.FullName;
            ViewBag.tags = tags;
            ViewBag.comment = comments;

            return View(blog);
        }
    }
}

