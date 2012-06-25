using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class RequestRegPago
    {
        public int flag { get; set; }
        public int id { get; set; }
        public decimal monto { get; set; }
        public decimal montoTotal { get; set; }
        public decimal pagoInicial { get; set; }
        public string doc { get; set; }
    }
}