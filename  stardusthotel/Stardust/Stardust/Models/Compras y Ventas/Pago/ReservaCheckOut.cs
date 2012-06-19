using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class ReservaCheckOut
    {
        public int id { get; set; }
        public decimal total { get; set; }
        public decimal subTotal { get; set; }
        public decimal IGV { get; set; }
        public decimal montPagado { get; set; }
        public decimal faltante { get; set; }
        public string fechaIni { get; set; }
        public string fechaFin { get; set; }
        public string fechaHoy { get; set; }
        public int idHotel { get; set; }
        public string nombre { get; set; }
        public string dni { get; set; }
        public int idUsuario { get; set; }
        public List<TipoDetalle> listaDetalles { get; set; }
        public ReservaCheckOut()
        {
            listaDetalles = new List<TipoDetalle>();
        }
    }

    public class TipoDetalle
    {
        public int id { get; set; }
        public string detalle { get; set; }
        public int cantidad { get; set; }
        public decimal pUnit { get; set; }
        public decimal totalDet { get; set; }        
    }
}
