using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class ProductoXAlmacenBean
    {

        [Display(Name = "Hotel")]
        public String Hotel { get; set; }
        public int idalmacen{get;set;}
        public int idhotel { get; set; }
        public List<ProductoAlmacen> listProdalmacen { get; set; }
    }
}