using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

namespace Stardust.Models
{
    public class PagoFacade
    {
        PagoService pagoService = new PagoService();

        public List<ReservaBean> GetReserva(string nombre)
        {
            return pagoService.GetReserva(nombre);
        }

        public Reserva ObtenerReserva(int id)
        {
            return pagoService.ObtenerReserva(id);
        }
    }
}
