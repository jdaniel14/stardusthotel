using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models{

    public class ProductoProveedor : ProductoBean
    {

        
        //[RegularExpression("/^[\-\+]?(([0-9]+)([\.,]([0-9]+))?|([\.,]([0-9]+))?)$/", ErrorMessage = "El valor ingresado es incorrecto")]
        public decimal precio { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado es incorrecto")]
        public int cantMaxima { get; set; }
        public bool estados { get; set; }
        public bool estado2 { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado es incorrecto")]
        public string precio2 { get; set; }
    }    
}