using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Stardust.Models
{
    public class ProductoxProveedorBean
    {
        [Display(Name = "Proveedor")]
        public String Proveedor { get; set; }

        public List<ProductoProveedor> listProdProv { get; set; }
     /*   [Display(Name = "Precio")]
        public List<int> Precio { get; set; }

        [Display(Name = "Cantidad Maxima")]
        public List<int> CantidadMax { get; set; }

        [Display(Name = "Productos")]
        public List<String> Producto { get; set; }*/

         // estado para guardar a la base de datos
    }
}