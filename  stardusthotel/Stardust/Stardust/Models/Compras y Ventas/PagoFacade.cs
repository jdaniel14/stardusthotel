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

        public ReservaCheckOut GetReserva(int id)
        {
            return pagoService.GetReserva(id);
        }
    }
}
