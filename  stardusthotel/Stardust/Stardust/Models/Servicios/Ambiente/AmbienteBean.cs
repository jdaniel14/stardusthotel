using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class AmbienteBean
    {
        public int id{ get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar nombre del Ambiente")]
        [RegularExpression("^[a-zA-Z áéíóúAÉÍÓÚÑñ]+$", ErrorMessage = "El nombre ingresado no es válido")]
        public string nombre { get; set; }

        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        [Display(Name = "Capacidad Máxima")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número entero positivo")]
        public int cap_maxima { get; set; }


        public decimal largo { get; set; }
        public decimal ancho { get; set; }
        public decimal precioXhora { get; set; }

        [Display(Name = "Piso")]
        [Range(0, 20, ErrorMessage = "El Número máximo de pisos es 20")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número entero")]
        public int piso { get; set; }

        [Display(Name = "Hotel")]
        [Required(ErrorMessage = "Es necesario asignar un Hotel al ambiente")]
        public int idHotel { get; set; }
        
        public string nombreHotel { get; set; }

        public string estado { get; set; }
    }
}