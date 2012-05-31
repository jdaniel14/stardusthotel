using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class EmpleadoBean
    {
        public int ID { get; set; }

        [Display( Name = "Nombre" ) ]
        public string nombreEmpleado { get; set; } // <------- SE DEBE JALAR DEL USUARIO
        
        [Display( Name = "Fecha de ingreso" ) ]
        public DateTime fechaIngreso { get; set; }

        [Display( Name = "Fecha de salida" ) ]
        public DateTime fechaSalida { get; set; }

        [Display( Name = "Estado" ) ]
        public string estado { get; set; }
    }
}