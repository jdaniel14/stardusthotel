using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class OrdenCompraBean
    {
        public int idOrdenCompra { get; set; }
        public int idproveedor { get; set; }
        public string estado { get; set; }
        public string fecha { get; set; }
        public List<DetalleOrdenCompra> detalle { get; set; }
    }
}