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

        [Display( Name = "Nombre" ) ]
        public String nombre { get; set; }

        [Display( Name = "Descripción" ) ]
        public String descripcion { get; set; }
    }
}