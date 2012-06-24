using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class OrdenProducto
    {
        [Display(Name = "Proveedor")]
        public string proveedor { get; set; }

        public int id { get; set; } //idproveedor

        public List<Producto> listaProducto { get; set; }
        
        [Display(Name = "IdHotel")]
        public int idhotel { get; set; }

        [Display(Name = "Hotel")]
        public string nombrehotel { get; set; }

        public Boolean estado2 { get; set; }
    }
}