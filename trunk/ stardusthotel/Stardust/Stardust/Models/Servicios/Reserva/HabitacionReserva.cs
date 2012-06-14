using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class HabitacionReserva
    {
        public int idHab{ get; set; }
        public int idTipoHabitacion { get; set; }
        public string numero { get; set; }
        public int piso { get; set; }
    }
}