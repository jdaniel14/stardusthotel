using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class UbicacionClienteBean
    {
        public int idReserva { get; set; }
        public String numero { get; set; }
        public int piso { get; set; }
        public String nombYApell { get; set; }        
        public String dni { get; set; }        
    }
}