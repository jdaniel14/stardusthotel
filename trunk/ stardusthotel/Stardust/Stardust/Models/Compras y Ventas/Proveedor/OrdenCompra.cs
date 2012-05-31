using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class OrdenCompras
    {
        public int id { get; set; }
        public string fecha { get; set; }
        public string estado { get; set; }
        public decimal precio { get; set; }
    }
}