using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class ConsultaReservaBean
    {
        public int idReserva { get; set; }
        public String fechaIni { get; set; }
        public String fechaFin { get; set; }
        public List<TipoHabCant> listaHab { get; set; }
        public String doc { get; set; }
        public String Nombre { get; set; }
        public String me { get; set; }
    }
}