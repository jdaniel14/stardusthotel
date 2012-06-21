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
        public int estado;

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

        public int idCliente { get; set; }
        public string nombreCliente { get; set; }


        public int idHotel { get; set; }
        public string nombreHotel { get; set; }
        public int estadoPago { get; set; }

        public decimal montoTotal { get; set; }
        public decimal pagoInicial { get; set; }
        public DateTime fechaRegistro { get; set; }
         

        }


}