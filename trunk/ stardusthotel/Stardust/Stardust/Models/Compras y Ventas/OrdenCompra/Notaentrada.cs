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
        
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado es incorrecto")]
        public int cantidadentrante { get; set; }

        public Boolean estado { get; set; }
    }
}