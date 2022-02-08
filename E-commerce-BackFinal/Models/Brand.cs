using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Models
{
    public class Brand
    {
        public int Id  { get; set; }
        [Required(ErrorMessage = "dont empty")]
        public string Name { get; set; }
        public List<CategoryBrand> CategoryBrand { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
