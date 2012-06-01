using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class HorarioEmpleadoBean
    {
        public int ID { get; set; }

        [Display( Name = "Fecha de inicio" ) ]        
        public DateTime fechaIni { get; set; }

        [Display( Name = "Fecha de Fin" ) ]
        public DateTime fechaFin { get; set; }

        [Display( Name = "Empleado" ) ]
        public int idEmpleado { get; set; }

        [Display( Name = "Empleado" ) ]
        public string nombreEmpleado { get; set; }

        public List<DetalleHorario> detalleHorario { get; set; }
    }
}