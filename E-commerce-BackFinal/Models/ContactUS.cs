using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Models
{
    public class ContactUS
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string MapUrl { get; set; }
        public string MobileNumber { get; set; }
        public string HotlineNumber { get; set; }
        public string Email { get; set; }
        public string SupportMail { get; set; }
        public string OpenClose { get; set; }
    }
}
