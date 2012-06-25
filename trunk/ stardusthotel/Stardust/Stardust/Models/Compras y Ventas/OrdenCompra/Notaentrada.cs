using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class Notaentrada:ProductoBean
    {
        public int cantidadrecibida { get; set; }
        public int cantidadsolicitada { get; set; }
        public int cantidadfaltante { get; set; }
        
        //[Range(0, 999, ErrorMessage = "El número mínimo de días es 1")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo números")]
        [Required(ErrorMessage = "Es necesario ingresar la cantidad entrante")]
        public int cantidadentrante { get; set; }

        public Boolean estado { get; set; }
    }
}