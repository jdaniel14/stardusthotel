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
        public List<ReservaBean> GetReserva(string nombre)
        {
            return pagoDAO.GetReserva(nombre);
        }

        public Reserva ObtenerReserva(int id)
        {
            return pagoDAO.ObtenerReserva(id);
        }
    }
}
