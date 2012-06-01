using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class DetalleHorario
    {
        public int ID { get; set; }

        [Display( Name = "Día" ) ]
        public string diaSemana { get; set; }

        [Display( Name = "Hora de entrada" ) ]
        public DateTime horaEntrada { get; set; } // <------------------- REVISAR ESTO CON LA BD

        [Display( Name = "Hora de salida" ) ]
        public DateTime horaSalida { get; set; } // <------------------- REVISAR ESTO CON LA BD

        public int idHorario { get; set; }
    }
}