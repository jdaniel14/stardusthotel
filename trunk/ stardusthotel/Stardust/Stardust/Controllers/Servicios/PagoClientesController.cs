using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stardust.Models;
using Stardust.Models.Servicios;
using System.Web.Mvc;
using ReportManagement;

namespace Stardust.Controllers.Servicios
{
    public class PagoClientesController : PdfViewController
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

        [HttpPost]
        public JsonResult registrarCheckOut(RequestCheckOut request)
        {
            MensajeBean me = pagoFacade.RegistrarCheckOut(request.idReserva);
            return Json(me);
        }
    
        [HttpPost]
        public JsonResult PagoAdelantado(RequestPagoAde request)
        {
            PagoAdelantadoBean res = new PagoAdelantadoBean();
            return Json(res);
        }

        public ActionResult PagarInicial()
        {
            return View();
        }

        public ActionResult GenerarDocumento(int id)
        {
            ReservaCheckOut reserva = pagoFacade.GetReserva(id);

            if(reserva.tipoDoc.Equals("DNI"))
                return this.ViewPdf("Boleta", "Documento", reserva);
            else
                return this.ViewPdf("Factura", "Documento", reserva);
        }
    }
}
