using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class TipoHabXHotel
    {
        public int idTipoHab { get; set; }
        public String nombreTipoHab { get; set; }
        public int numPos { get; set; }
        public decimal precio { get; set; }
    }
}