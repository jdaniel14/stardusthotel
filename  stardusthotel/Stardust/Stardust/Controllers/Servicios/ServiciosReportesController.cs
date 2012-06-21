using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

namespace Stardust.Controllers.Servicios
{
    public class ServiciosReportesController : Controller
    {
        //
        // GET: /ServiciosReportes/

        PagoFacade pagoFacade = new PagoFacade();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReporteHabitaciones()
        {
            return View();
        }

        public ActionResult ReporteCliente()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListaHabitacion(int idHotel,string fechaIni,string fechaFin)
        {
            List<ListaHabitacion> listaHab = new List<ListaHabitacion>();
            listaHab = pagoFacade.listaHabitacion(idHotel, fechaIni, fechaFin);
            return Json(listaHab);
        }
    }
}
