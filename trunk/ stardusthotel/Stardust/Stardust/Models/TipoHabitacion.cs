using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations ;

namespace Stardust.Models
{
    public class TipoHabitacion
    {
        [Key]
        [Column( "idTipoHabitacion" ) ]
        public int TipoHabitacionID { get; set; }

        [Display( Name = "Nombre" ) ]
        [Required]
        [Column( "nombre" ) ]
        public string nombre { get; set; }

        [Display( Name = "Descripción" ) ]
        [Required]
        [Column( "descripcion" ) ]
        public string descripcion { get; set; }
    }

}