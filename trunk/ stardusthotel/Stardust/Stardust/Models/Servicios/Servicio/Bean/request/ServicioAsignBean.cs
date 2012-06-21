using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ServicioAsignBean
    {
        public int idSer {get; set;}
        public int nroRes{get; set;}//servicio o reserva
        public String dni{get; set;}
        public int nRecib{get; set;}
        public Decimal monto{get; set;}
        public int flagTipo{get; set;}//0 evento 1 reserva
        public int idHotel { get; set; }
    }
}