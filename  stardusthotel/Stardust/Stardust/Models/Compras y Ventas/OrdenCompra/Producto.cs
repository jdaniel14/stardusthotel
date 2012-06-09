using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class Producto
    {
        public string id { get; set; }
        public string Nombre { get; set; }
        public int cantidad { get; set; }
        public int stockActual { get; set; }
        public int stockMinimo { get; set; }
        public int stockMaximo { get; set; }
        public double precio { get; set; }
    }
}