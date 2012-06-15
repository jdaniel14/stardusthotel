using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class TipoHabDisp
    {
        public int idTipoHab { get; set; }
        public String nombreTipoHab { get; set; }        
        public decimal precioTipoHab { get; set; }
        public String desc { get; set; }
        public int nroHab { get; set; }
        public List<ReservaHabitacionBean> listaDisp { get; set; }

    }
}