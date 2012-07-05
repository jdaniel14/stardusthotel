using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class Evento
    {
        public int id { get; set; }
        public string fechaIni { get; set; }
        public string fechaFin { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string usuario { get; set; }
        public string me { get; set; }
        public string hotel { get; set; }
        public string dni { get; set; }
        public string tipoDoc { get; set; }
        public decimal faltante { get; set; }
        public decimal montPagado { get; set; }
        public decimal total { get; set; }
        public decimal igv { get; set; }
        public decimal subTotal { get; set; } 
        public int idDocPago { get; set; }
        public List<EventoDetalle> listaDetalle { get; set; }

        public Evento()
        {
            listaDetalle = new List<EventoDetalle>();
        }
    }

    public class EventoDetalle
    {
        public string detalle { get; set; }
        public int cantidad { get; set; }
        public decimal precioUnit { get; set; }
        public decimal total { get; set; }
    }
}