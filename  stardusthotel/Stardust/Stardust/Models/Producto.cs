using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class Producto
    {
        public int ID { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Descripción")]
        public string Nombre { get; set; }
        public string descripcion { get; set; }
        [Display(Name = "Stock Mínimo")]
        public int stock_minimo { get; set; }
        [Display(Name = "Stock Actual")]
        public int stock_actual { get; set; }       
    }
}