using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class DocumentoPagoBean
    {
        public int idDocPago { get; set; }
        public int idUsuario {get; set;}
        public Decimal montoTotal {get; set;}
        public Decimal montoFaltante {get; set;}
        public DateTime fechaRegistro {get; set;}
        public Decimal subTotal {get; set;}
        public Decimal igv{get; set;}
        public Decimal interes {get; set;}
        public int nroCuotas {get; set;}
        public String tipoDocPago {get; set;}
        public int estado {get; set;}
        public int idReserva {get; set;}
        public int idEvento{get; set;}
        public String me { get; set; }
    }
}