using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class OrdenProducto
    {
        public string proveedor { get; set; }
        public int id { get; set; }
        public List<Producto> listaProducto { get; set; }
    }
}