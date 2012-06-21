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

        public ReservaCheckOut GetReserva(int id)
        {
            return pagoService.GetReserva(id);
        }

        public MensajeBean RegistrarCheckOut(int id)
        {
            return pagoService.RegistrarCheckOut(id);
        }

        public PagoAdelantadoBean PagoAdelantado(RequestPagoAde request)
        {
            return pagoService.PagoAdelantado(request);
        }
    }
}
