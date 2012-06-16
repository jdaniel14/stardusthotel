using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

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
    public class Reserva
    {
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
        public int numCuotas { get; set; }
        public List<TipoHab> listaHab { get; set; }
        public Reserva()
        {
            listaHab = new List<TipoHab>();
        }
    }

    public class TipoHab
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
    }
}
