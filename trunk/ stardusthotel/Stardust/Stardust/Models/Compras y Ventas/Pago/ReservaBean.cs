using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stardust.Models
{
    public class ReservaBean
    {
        public int id { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaCheckOut { get; set; }
        public string estado { get; set; }
        public decimal pagoIni { get; set; }
        public decimal total { get; set; }
        public int idHotel { get; set; }
        public string nombre { get; set; }
        public int idUsuario { get; set; }
    }
}
