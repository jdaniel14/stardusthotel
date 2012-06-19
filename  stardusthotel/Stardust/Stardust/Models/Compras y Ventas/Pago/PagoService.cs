using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stardust.Models
{
    public class PagoService
    {
        PagoDAO pagoDAO = new PagoDAO();
        public ReservaCheckOut GetReserva(int id)
        {
            return pagoDAO.GetReserva(id);
        }

        public string RegistrarPagoContado(Reserva reserva)
        {
            return pagoDAO.RegistrarPagoContado(reserva);
        }
    }
}
