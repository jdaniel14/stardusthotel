using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models.Servicios;

namespace Stardust.Models
{
    public class PagoService
    {
        PagoDAO pagoDAO = new PagoDAO();
        public ReservaCheckOut GetReserva(int id)
        {
            return pagoDAO.GetReserva(id);
        }

        public MensajeBean RegistrarCheckOut(int id)
        {
            return pagoDAO.RegistrarCheckOut(id);
        }

        public PagoAdelantadoBean PagoAdelantado(RequestPagoAde request)
        {
            return pagoDAO.PagoAdelantado(request);
        }

        public List<ListaHabitacion> listaHabitacion(string fechaIni, string fechaFin)
        {
            return pagoDAO.listaHabitacion(fechaIni, fechaFin);
        }
    }
}
