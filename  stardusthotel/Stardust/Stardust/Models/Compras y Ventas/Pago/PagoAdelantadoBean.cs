using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class PagoAdelantadoBean
    {
        public string data { get; set; }
        public string doc { get; set; }
        public string nom { get; set; }
        public int estado { get; set; }
        public decimal montoInicial { get; set; }
        public string mensaje { get; set; }
        public decimal montoTotal { get; set; }
    }
}