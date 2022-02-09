﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string  Title { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        
        public DateTime Date { get; set; }
        public List<BlogPhoto> BlogPhotos { get; set; }
        [NotMapped]
        
        public IFormFile[] Photos { get; set; }
    }
}
