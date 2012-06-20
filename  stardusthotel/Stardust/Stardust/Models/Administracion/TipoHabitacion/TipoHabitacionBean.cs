using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class TipoHabitacionBean
    {
        [Key]
        public int ID { get; set; }

        [Display( Name = "Tipo de Habitación" ) ]
        [Required( ErrorMessage = "Ingrese un nombre para el tipo de habitación" ) ]
        public String nombre { get; set; }

        [Display( Name = "Descripción" ) ]
        [Required(ErrorMessage = "Ingrese una descripción para el tipo de habitación")]
        public String descripcion { get; set; }
    }
}