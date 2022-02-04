using E_commerce_BackFinal.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly Context _context;

        public HeaderViewComponent(Context context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
