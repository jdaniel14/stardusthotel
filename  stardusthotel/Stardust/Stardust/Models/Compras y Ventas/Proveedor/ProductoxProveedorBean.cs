using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ProductoxProveedorBean
    {
        public String Proveedor { get; set; }
        public List<int> Precio { get; set; }
        public List<int> CantidadMax { get; set; }
        public List<ProductoBean> Producto { get; set; }
    }
}