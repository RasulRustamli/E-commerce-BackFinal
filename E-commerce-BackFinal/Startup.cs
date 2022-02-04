using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireLowercase = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = true;

                opt.User.RequireUniqueEmail = true;

                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                opt.Lockout.MaxFailedAccessAttempts = 3;
                //opt.Lockout.AllowedForNewUsers = true;


            }).AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();
            services.AddControllersWithViews();
            services.AddDbContext<Context>(opt =>
            {
                opt.UseSqlServer(_config["ConnectionStrings:DefaultConnection"]);
                //opt.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });
            services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromMinutes(10);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   "areas",
                  "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                 );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Contact}/{action=Index}/{id?}");
            });
        }
    }
}
