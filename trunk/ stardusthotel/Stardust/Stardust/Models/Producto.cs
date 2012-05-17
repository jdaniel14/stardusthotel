using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Stardust.Models
{
    public class Producto
    {
        public int ID { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
        public int estado { get; set; }        
    }
}