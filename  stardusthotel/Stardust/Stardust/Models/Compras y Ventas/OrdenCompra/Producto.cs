using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class Producto
    {
        public string id { get; set; } // idproducto
        public string Nombre { get; set; }

        public int idproducto { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número")]
        public int cantidad { get; set; }
        
        public int stockActual { get; set; }
        
        public int stockMinimo { get; set; }
        
        public int stockMaximo { get; set; }
        
        public decimal precio { get; set; }

        public Boolean estado { get; set; }

        public Boolean estadoguardar { get; set; }
    }
}