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

        public ActionResult ListaHabitacion(int idHotel,string fechaIni,string fechaFin)
        {
            return View(pagoFacade.listaHabitacion(idHotel,fechaIni,fechaFin));
        }
    }
}
