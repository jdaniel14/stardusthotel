using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class ReservaRequest
    {
        public int idHotel { get; set; }
        public String fechaIni { get; set; }
        public String fechaFin { get; set; }
    }
}