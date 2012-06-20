using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class ClienteReservaBean
    {
        public String tipoDoc{get; set;} 
        public String nroDoc{get; set;} 
        public String nomb{get; set;} 
        public String apell{get; set;}
        public String razSoc{get; set;}
        public String email{get; set;} 
        public String telf{get; set;} 
        public String tipoTarj{get; set;}
        public String nroTarj{get; set;}
    }
}