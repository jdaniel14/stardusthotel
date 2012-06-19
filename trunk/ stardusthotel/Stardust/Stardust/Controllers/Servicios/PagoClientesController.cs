using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stardust.Models;
using System.Web.Mvc;

namespace Stardust.Controllers.Servicios
{
    public class PagoClientesController : Controller
    {
        PagoFacade pagoFacade = new PagoFacade();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult checkOut()
        {
            return View();
        }

        [HttpPost]
        public JsonResult registroCheckOut(RequestCheckOut request)
        {
            ReservaCheckOut res = pagoFacade.GetReserva(request.idReserva);
            System.Diagnostics.Debug.WriteLine("NOMBRE DEL CHECK OUT : " + res.nombre);
            return Json(res);
        }
    }
}
