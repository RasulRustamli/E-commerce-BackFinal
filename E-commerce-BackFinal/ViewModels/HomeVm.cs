using E_commerce_BackFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.ViewModels
{
    public class HomeVm
    {
        public List<CompanySlider> companySliders { get; set; }
        public List<Service> services { get; set; }
    }
}
