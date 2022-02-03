using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly Context _context;
        public FooterViewComponent(Context context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ContactUS contact = _context.Contacts.FirstOrDefault();
            return View(await Task.FromResult(contact));
        }
    }
}
