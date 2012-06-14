using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class AmbienteBean
    {
        public int id{ get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int cap_maxima { get; set; }
        public decimal largo { get; set; }
        public decimal ancho { get; set; }
        public decimal precioXhora { get; set; }
        public int piso { get; set; }
        public string estado { get; set; }
    }
}