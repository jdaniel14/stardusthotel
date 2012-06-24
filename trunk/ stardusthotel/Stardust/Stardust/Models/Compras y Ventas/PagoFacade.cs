using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using Stardust.Models.Servicios;

namespace Stardust.Models
{
    public class PagoFacade
    {
        PagoService pagoService = new PagoService();
        PagoDAO pagoDAO = new PagoDAO();

        public ReservaCheckOut GetReserva(int id)
        {
            return pagoService.GetReserva(id);
        }

        public MensajeBean RegistrarPagoAdelantado(RequestRegPago request)
        {
            return pagoDAO.RegistrarPagoAdelantado(request);
        }

        public PagoAdelantadoBean PagoAdelantado(RequestPagoAde request)
        {
            return pagoDAO.PagoAdelantado(request);
        }

        public ReservaCheckOut GetReserva2(int id)
        {
            return pagoDAO.GetReserva2(id);
        }

        public MensajeBean RegistrarCheckOut(int id)
        {
            return pagoService.RegistrarCheckOut(id);
        }

        public List<ListaHabitacion> listaHabitacion(int idHotel,string fechaIni, string fechaFin)
        {
            return pagoService.listaHabitacion(idHotel,fechaIni, fechaFin);
        }
    }
}