using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class DatosReservaBean
    {
        public int idReserva{get;set;}
        public String fechaLlegada { get; set; }
        public String fechaSalida { get; set; }
        public String fechaRegistro { get; set; }
        public String doc { get; set; }
        public String nomb { get; set; }
        public String me { get; set; }
    }
}