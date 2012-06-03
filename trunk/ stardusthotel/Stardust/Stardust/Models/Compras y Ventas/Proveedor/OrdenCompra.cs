using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class OrdenCompraDetalle
    {
        public int id { get; set; }
        public int idOC { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public int preciouni { get; set; }
        public string nombre { get; set; }
    }
    public class OrdenCompra
    {
        public int id { get; set; }
        public string fecha { get; set; }
        public string estado { get; set; }
        public decimal precio { get; set; }
        public int idProv { get; set; }
    }
    public class OrdenCompras
    {
        public int id { get; set; }        
        public string nombre { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public List<OrdenCompra> listaOC { get; set; }
        public List<OrdenCompraDetalle> listaOCDetalle { get; set; }

        public OrdenCompras()
        {
            listaOC = new List<OrdenCompra>();
            listaOCDetalle = new List<OrdenCompraDetalle>();
        }
    }
}