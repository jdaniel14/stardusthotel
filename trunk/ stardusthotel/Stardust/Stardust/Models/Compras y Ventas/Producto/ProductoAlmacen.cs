using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class ProductoAlmacen:ProductoBean
    {

        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado es incorrecto")]
        public int stockmaximo { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado es incorrecto")]
        public int stockactual { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado es incorrecto")]
        public int stockminimo { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado es incorrecto")]
        public int stocksaliente { get; set; }
        public bool estados { get; set; }
        public bool estado2 { get; set; }
    }
}