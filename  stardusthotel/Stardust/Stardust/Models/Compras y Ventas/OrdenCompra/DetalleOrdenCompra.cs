using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class DetalleOrdenCompra:ProductoBean
    {
        public int Cantidad { get; set; }
        public decimal precio { get; set; }
    }
}