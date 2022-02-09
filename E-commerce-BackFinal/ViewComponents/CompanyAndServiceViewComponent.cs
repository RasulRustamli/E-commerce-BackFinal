using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Models;
using E_commerce_BackFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.ViewComponents
{
    public class CompanyAndServiceViewComponent:ViewComponent
    {
        private readonly Context _context;

        public CompanyAndServiceViewComponent(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<CompanySlider> companySliders = _context.CompanySliders.ToList();
            List<Service> services = _context.Services.ToList();

            CompanySliderService companySliderServiceVM = new CompanySliderService();
            companySliderServiceVM.CompanySliders = companySliders;
            companySliderServiceVM.Services = services;

            return View(await Task.FromResult(companySliderServiceVM));
        }


    }
}
