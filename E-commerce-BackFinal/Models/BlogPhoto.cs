﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Models
{
    public class BlogPhoto
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public string VideoUrl { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

    }
}
