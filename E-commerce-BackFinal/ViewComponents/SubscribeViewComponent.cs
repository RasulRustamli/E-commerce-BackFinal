using E_commerce_BackFinal.DAL;
using E_commerce_BackFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.ViewComponents
{
    public class SubscribeViewComponent:ViewComponent
    {
        private readonly Context _context;
        public SubscribeViewComponent(Context context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Subscribe subscribe = _context.Subcribes.FirstOrDefault();
            return View(await Task.FromResult(subscribe));
        }
    }
}
