﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, MinLength(5)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public double ExTax { get; set; }
        [Required]
        public string ProductCode { get; set; }
        public bool Availibility { get; set; }
        [Required]
        public int Quantity { get; set; }
        public bool Featured { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        public List<ProductPhoto> productPhotos { get; set; }
        public List<ProductTag> ProductTags { get; set; }
        public List<ColorProduct> ColorProducts { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<SalesProduct> SalesProducts { get; set; }

        [NotMapped]
        [Required]
        public IFormFile[] Photos { get; set; }

    }
}
