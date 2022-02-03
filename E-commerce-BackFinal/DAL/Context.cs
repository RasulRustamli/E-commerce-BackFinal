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

    }
}
