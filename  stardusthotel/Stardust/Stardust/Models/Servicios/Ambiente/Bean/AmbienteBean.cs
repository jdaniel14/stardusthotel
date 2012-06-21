using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class AmbienteBean
    {
        [Key]
        public int id{ get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar nombre del Ambiente")]
        [RegularExpression("^[a-zA-Z áéíóúAÉÍÓÚÑñ]+$", ErrorMessage = "El nombre ingresado no es válido")]
        public string nombre { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Debe ingresar la descripción del Ambiente")]
        public string descripcion { get; set; }

        [Display(Name = "Capacidad Máxima")]
        [Required(ErrorMessage = "Debe ingresar la capacidad máxima del Ambiente")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número entero y positivo")]
        public int cap_maxima { get; set; }

        [Display(Name = "Largo")]
        [Required(ErrorMessage = "Debe ingresar el largo del Ambiente")]
        public decimal largo { get; set; }

        [Display(Name = "Ancho")]
        [Required(ErrorMessage = "Debe ingresar el ancho del Ambiente")]
        public decimal ancho { get; set; }

        [Display(Name = "Precio por Hora")]
        [Required(ErrorMessage = "Debe ingresar el precio por hora del Ambiente")]
        public decimal precioXhora { get; set; }

        [Display(Name = "Piso")]
        [Required(ErrorMessage = "Debe ingresar el piso del Ambiente")]
        [Range(0, 9, ErrorMessage = "El número máximo de pisos es 20")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número entero positivo")]
        public int piso { get; set; }

        [Display(Name = "Estado")]
        [RegularExpression("^[a-zA-Z áéíóúAÉÍÓÚÑñ]+$", ErrorMessage = "El nombre ingresado no es válido")]
        public string estado { get; set; }

        [Display(Name = "Hotel")]
        [Required(ErrorMessage = "Es necesario asignar un Hotel al Ambiente")]
        public int idHotel { get; set; }

        [Display(Name = "Nombre del Hotel")]
        public string nombreHotel { get; set; }


        
        public String me { get; set; }
    }
}