using E_commerce_BackFinal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.DAL
{
    public class Context: IdentityDbContext<AppUser>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<CompanySlider> CompanySliders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ContactUS> Contacts { get; set; }
        public DbSet<Subscribe> Subcribes { get; set; }
        public DbSet<SubscribeMail> SubscribeMails { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CategoryBrand> CategoryBrands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ColorProduct> ColorProducts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ProductRelation> ProductRelations { get; set; }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogPhoto> BlogPhotos { get; set; }


    }
}
