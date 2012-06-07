using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Stardust.Models
{
    public class EventoBean
    {
        public int ID { get; set; }

        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "Fecha Inicial")]
        public string fechaIni { get; set; }

        [Display(Name = "Fecha Final")]
        public string fechaFin { get; set; }

        [Display(Name = "Hora Inicial")]
        public string horaIni { get; set; }

        [Display(Name = "Hora Final")]
        public string horaFin { get; set; }

        [Display(Name = "NumeroParticipantes")]
        public int nroParticipantes { get; set; }

        public string tiempo { get; set; }



    }
}