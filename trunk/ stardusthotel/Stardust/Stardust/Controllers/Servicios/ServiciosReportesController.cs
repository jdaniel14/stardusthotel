using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using log4net;

namespace Stardust.Controllers.Servicios
{
    public class ServiciosReportesController : Controller
    {
        //
        private static ILog log = LogManager.GetLogger(typeof(ServiciosReportesController));
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
            System.Diagnostics.Debug.WriteLine("Hotel : " + idHotel);
            List<ListaHabitacion> listaHab = new List<ListaHabitacion>();
            listaHab = pagoFacade.listaHabitacion(idHotel, fechaIni, fechaFin);

            for (int i = 0; i < listaHab.Count; i++)
            {
                List<ListaHabitacionEstado> listaDetalle = listaHab.ElementAt(i).listaFechas;
                for (int j = 0; j < listaDetalle.Count; j++)
                    System.Diagnostics.Debug.WriteLine(listaDetalle.ElementAt(j).estado);

            }
                return Json(listaHab);
        }
    }
}
