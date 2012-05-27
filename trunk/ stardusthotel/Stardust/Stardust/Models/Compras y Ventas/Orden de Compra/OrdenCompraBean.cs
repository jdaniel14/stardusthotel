using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Stardust.Models
{
    public class Producto 
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
    }
    public class Proveedor
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
    }
    
    public class OrdenCompraBean
    {
        public int idOrdenCompra { get; set; }
        public int idproveedor { get; set; }
        
        [Display(Name = "Proveedor")]
        public string nombreproveedor { get; set; }
        
        [Display(Name = "Estado")]
        public string estado { get; set; }
        
        [Display(Name = "Fecha")]
        public string fecha { get; set; }
        
        public List<DetalleOrdenCompra> detalle { get; set; }
    }
}