using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ReservaAmbBean
    {
        public int idEvento { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public int estado { get; set; }
        public Decimal pagoInicial { get; set; }
        public Decimal montoTotal { get; set; }
        public int nroPersonas { get; set; }
        public int idHotel { get; set; }
        public int idUsuario { get; set; }
        public DateTime fechaRegistro { get; set; }
        public int estadoPago { get; set; }
        public String me { get; set; }
    }
}