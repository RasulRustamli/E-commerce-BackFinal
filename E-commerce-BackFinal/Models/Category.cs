﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "dont empty")]
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public bool IsFeature { get; set; }
        public bool IsDeleted { get; set; }
        public string PhotoUrl { get; set; }
        public Category MainCategory { get; set; }
        public List<Category> SubCategory { get; set; }
        public List<CategoryBrand> CategoryBrand { get; set; }
        

        [NotMapped]
        public IFormFile Photo { get; set; }


    }
}
