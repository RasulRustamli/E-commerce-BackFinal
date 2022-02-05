using E_commerce_BackFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.ViewModels
{
    public class ContactUsVm
    {
        public ContactUS Contact { get; set; }
        public AppUser User { get; set; }
        public Message Message { get; set; }
    }
}
