using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class NotaEntradaBean
    {
        public int idordencompra { get; set; }

        public   string nombreproveedor { get; set; }
        public string estado { get; set; } 
        public string fechaemitida{get;set;}
        public int idproveedor { get; set; }
        public decimal preciototal { get; set; }
        public List<Notaentrada> detallenotaentrada { get; set; }


    }
}