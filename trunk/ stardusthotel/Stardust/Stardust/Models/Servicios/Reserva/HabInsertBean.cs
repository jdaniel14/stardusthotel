using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class HabInsertBean
    {
        public int tipo { get; set; }
        public int cant { get; set; }
        public List<HabitacionReserva> list { get; set; }
    }
}