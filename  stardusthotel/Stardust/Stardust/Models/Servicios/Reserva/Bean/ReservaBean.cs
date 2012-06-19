using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class UsuarioBean
    {
        public int idReserva { get; set; }
        public DateTime fechaLlegada { get;set; }
        public DateTime fechaSalida { get; set; }
        public DateTime fechaCheckOut { get; set; }
        public int estado { get; set; }
        public Decimal pagoInicial { get; set; }
        public Decimal montoTotal { get; set; }
        public int nroPersonas { get; set; }
        public int idHotel { get;set; }
        public int idUsuario { get;set; }
        public DateTime fechaRegistro { get; set; }
        public int estadoPago { get; set; }
    }
}