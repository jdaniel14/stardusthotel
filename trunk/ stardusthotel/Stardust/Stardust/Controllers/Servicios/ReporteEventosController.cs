using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportManagement;
using Stardust.Models;
using Stardust.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Stardust.Controllers.Servicios
{
    public class ReporteEventosController :PdfViewController
    {
        AmbienteFacade eventoFacade = new AmbienteFacade();

        public ActionResult Evento()
        {
            ViewBag.listaHoteles = new HotelFacade().getHoteles(); 
            return View();
        }

        [HttpPost]
        public ActionResult Evento(string idHotel, string estadoPago)
        {

            int A;            
            if (!String.IsNullOrEmpty(idHotel)) A = Convert.ToInt32(idHotel);
            else A = 0;
            int idpago = Convert.ToInt32(estadoPago);

            return RedirectToAction("ListaEvento",new {id=idpago,idHotel=idHotel});
        }

        public ActionResult ListaEvento(int id, int idHotel)
        {
            ViewBag.listaHoteles = new HotelFacade().getHoteles();
            //ViewBag.listaClientes= new ClienteFacade().
            return View(eventoFacade.ListarEvento(idHotel,id));
            //return RedirectToAction("ListarOC");
        }

        public ActionResult Nada(int idhotel,int id)
        {
            return this.ViewPdf("", "ReporteEventos", eventoFacade.ListarEvento(idhotel,id));
        }
    }
}