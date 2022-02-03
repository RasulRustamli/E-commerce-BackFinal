using E_commerce_BackFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.ViewModels
{
    public class HomeVm
    {
        public IEnumerable<CompanySlider> companySliders { get; set; }
    }
}
