﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ColorProduct> ColorProducts { get; set; }

    }
}
