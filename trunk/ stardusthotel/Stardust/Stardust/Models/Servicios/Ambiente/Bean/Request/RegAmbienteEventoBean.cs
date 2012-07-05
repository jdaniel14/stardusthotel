using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class RegAmbienteEventoBean
    {
        public int idHotel { get; set; }
        public String fechaIni { get; set; }
        public String fechaFin { get; set; }
        public String coment { get; set; }
        public Decimal total { get; set; }
        public ClienteReservaBean client { get; set; }
        public List<AmbienteBean> listAmbi { get; set; }
        public EventoResBean evento { get; set; }
        public String pass { get; set; }               
        public int idUsuario { get; set; }
        public int tipoRegistro { get; set; }
    }
}