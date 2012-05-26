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
    }
}