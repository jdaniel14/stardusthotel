using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ProductoAlmacen:ProductoBean
    {
       
        public int stockmaximo { get; set; }
        public int stockactual { get; set; }
        public int stockminimo { get; set; }
        public bool estados { get; set; }
        public bool estado2 { get; set; }
    }
}