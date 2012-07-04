using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportManagement;
using Stardust.Models;
using System.Data.SqlClient;
using System.Configuration;
using log4net;

namespace Stardust.Controllers.Servicios
{
    public class ReporteEventosController :PdfViewController
    {
        AmbienteFacade eventoFacade = new AmbienteFacade();
        private static ILog log = LogManager.GetLogger(typeof(ReporteEventosController));

        public ActionResult Evento()
        {
            ViewBag.listaHoteles = new HotelFacade().getHoteles(); 
            return View();
        }

        [HttpPost]
        public ActionResult Evento( string estadoPago)
        {

            //int A;            
            //if (!String.IsNullOrEmpty(idHotel)) A = Convert.ToInt32(idHotel);
            //else A = 0;
            int idpago = Convert.ToInt32(estadoPago);

            return RedirectToAction("ListaEvento",new {id=idpago});
        }

        public ActionResult ListaEvento(int id)
        {
           // ViewBag.listaHoteles = new HotelFacade().getHoteles();
            //ViewBag.listaClientes= new ClienteFacade().
            return View(eventoFacade.ListarEvento(id));
            //return RedirectToAction("ListarOC");
        }

        public ActionResult Nada(int id)
        {
            return this.ViewPdf("", "ReporteEventos", eventoFacade.ListarEvento(id));
        }
    }
}