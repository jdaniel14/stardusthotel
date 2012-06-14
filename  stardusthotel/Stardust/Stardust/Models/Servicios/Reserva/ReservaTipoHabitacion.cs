using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class ReservaTipoHabitacion
    {
        public int idTipoHabitacion{get;set;}
        public String nombre { get; set; }
        public decimal precioBaseXDia { get; set; }
        public String descripcion { get; set; }
    }
}