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
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaCheckOut { get; set; }
        public decimal total { get; set; }
        public decimal subTotal { get; set; }
        public decimal IGV { get; set; }
        public decimal montPagado { get; set; }
        public decimal faltante { get; set; }
        public DateTime fechaIni { get; set; }
        public DateTime fechaFin { get; set; }
        public DateTime fechaHoy { get; set; }
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
    public class Reserva
    {
        public string numDoc { get; set; }
        public string tipoDoc { get; set; }
        public int id { get; set; }
        public int idHotel { get; set; }
        public int idUsuario { get; set; }
        [Display(Name = "Hotel")]
        public string hotel { get; set; }
        [Display(Name = "Cliente" )]
        public string usuario { get; set; }
        public decimal pagoIni { get; set; }
        public decimal subTotal { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public decimal interes { get; set; }
        [Display(Name="Ingrese el monto")]
        public decimal pagado { get; set; }
        public int numCuotas { get; set; }
        public int estado { get; set; }
        public int estadoPago { get; set; }
        [Display(Name="Monto a pagar")]
        public decimal apagar { get; set; }
        public DateTime fechaLlegada { get; set; }
        public DateTime fechaSalida { get; set; }
        [Display(Name="Monto pagado")]
        public decimal pago { get; set; }
        //public List<TipoHab> listaHab { get; set; }
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
