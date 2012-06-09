﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models{

    public class ProductoProveedor : ProductoBean
    {
        public decimal precio { get; set; }
        public int cantMaxima { get; set; }
        public bool estados { get; set; }
        public bool estado2 { get; set; }
        public string precio2 { get; set; }
    }    
}